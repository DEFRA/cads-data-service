using Cads.Cds.StorageBridge.Endpoints;
using Cads.Cds.StorageBridge.Endpoints.Responses;
using FluentAssertions;
using System.Text;

namespace Cads.Cds.StorageBridge.Tests.Unit.Endpoints;

public class StorageRowSliceReaderTests
{
    [Fact]
    public async Task ReadAsync_SliceFromMiddle_ShouldReturnRequestedRowsOnly()
    {
        var slice = await ReadAsync("row1\nrow2\nrow3\nrow4\nrow5", startRow: 2, rowCount: 2, delimiter: "\n");

        slice.Rows.Should().Equal("row2", "row3");
        slice.ReachedEnd.Should().BeFalse();
    }

    [Fact]
    public async Task ReadAsync_SliceReachingLastRow_ShouldFlagReachedEnd()
    {
        var slice = await ReadAsync("row1\nrow2\nrow3", startRow: 2, rowCount: 10, delimiter: "\n");

        slice.Rows.Should().Equal("row2", "row3");
        slice.ReachedEnd.Should().BeTrue();
    }

    [Fact]
    public async Task ReadAsync_TrailingDelimiter_ShouldNotProduceEmptyFinalRow()
    {
        var slice = await ReadAsync("row1\nrow2\n", startRow: 1, rowCount: 10, delimiter: "\n");

        slice.Rows.Should().Equal("row1", "row2");
        slice.ReachedEnd.Should().BeTrue();
    }

    [Fact]
    public async Task ReadAsync_StartRowPastEndOfStream_ShouldReturnNoRows()
    {
        var slice = await ReadAsync("row1\nrow2", startRow: 10, rowCount: 5, delimiter: "\n");

        slice.Rows.Should().BeEmpty();
        slice.ReachedEnd.Should().BeTrue();
    }

    [Fact]
    public async Task ReadAsync_EmptyStream_ShouldReturnNoRows()
    {
        var slice = await ReadAsync(string.Empty, startRow: 1, rowCount: 5, delimiter: "\n");

        slice.Rows.Should().BeEmpty();
        slice.ReachedEnd.Should().BeTrue();
    }

    [Fact]
    public async Task ReadAsync_MultiCharacterDelimiterAcrossReads_ShouldSplitCorrectly()
    {
        // One byte per read, so the \r\n delimiter arrives split across reads.
        var stream = new DripStream(Encoding.UTF8.GetBytes("row1\r\nrow2\r\nrow3"));

        var slice = await StorageRowSliceReader.ReadAsync(
            stream, startRow: 1, rowCount: 10, "\r\n", TestContext.Current.CancellationToken);

        slice.Rows.Should().Equal("row1", "row2", "row3");
        slice.ReachedEnd.Should().BeTrue();
    }

    [Fact]
    public async Task ReadAsync_SliceCompleteEarly_ShouldStopReadingTheStream()
    {
        // The stream serves the first rows and then throws on any further read;
        // completing the slice must not trigger that read.
        var stream = new ThrowAfterFirstReadStream(Encoding.UTF8.GetBytes("row1\nrow2\nrow3\n"));

        var slice = await StorageRowSliceReader.ReadAsync(
            stream, startRow: 1, rowCount: 2, "\n", TestContext.Current.CancellationToken);

        slice.Rows.Should().Equal("row1", "row2");
        slice.ReachedEnd.Should().BeFalse();
    }

    [Fact]
    public async Task ReadAsync_NoDelimiterWithinMaxRowChars_ShouldThrow()
    {
        var body = new string('x', StorageRowSliceReader.MaxRowChars + 1024);

        var read = () => ReadAsync(body, startRow: 1, rowCount: 1, delimiter: "\n");

        await read.Should().ThrowAsync<InvalidDataException>();
    }

    private static Task<StorageRowSliceResponse> ReadAsync(
        string content, int startRow, int rowCount, string delimiter) =>
        StorageRowSliceReader.ReadAsync(
            new MemoryStream(Encoding.UTF8.GetBytes(content)),
            startRow,
            rowCount,
            delimiter,
            TestContext.Current.CancellationToken);

    /// <summary>Serves one byte per read call.</summary>
    private sealed class DripStream(byte[] data) : Stream
    {
        private int _position;

        public override bool CanRead => true;
        public override bool CanSeek => false;
        public override bool CanWrite => false;
        public override long Length => data.Length;
        public override long Position { get => _position; set => throw new NotSupportedException(); }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (_position >= data.Length || count == 0)
            {
                return 0;
            }

            buffer[offset] = data[_position++];
            return 1;
        }

        public override void Flush() { }
        public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();
        public override void SetLength(long value) => throw new NotSupportedException();
        public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();
    }

    /// <summary>Serves all data on the first read and throws on any later read.</summary>
    private sealed class ThrowAfterFirstReadStream(byte[] data) : Stream
    {
        private bool _read;

        public override bool CanRead => true;
        public override bool CanSeek => false;
        public override bool CanWrite => false;
        public override long Length => data.Length;
        public override long Position { get => 0; set => throw new NotSupportedException(); }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (_read)
            {
                throw new InvalidOperationException("The stream was read past the requested slice.");
            }

            _read = true;
            data.CopyTo(buffer.AsSpan(offset));
            return data.Length;
        }

        public override void Flush() { }
        public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();
        public override void SetLength(long value) => throw new NotSupportedException();
        public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();
    }
}
