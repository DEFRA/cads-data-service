using Cads.Cds.BuildingBlocks.Application.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;
using FluentAssertions;

namespace Cads.Cds.BuildingBlocks.Application.Tests.Unit.OpenXml;

public class OpenXmlHelperTests
{
    [Fact]
    public void ReIndexRow_ShouldUpdateRowNumber()
    {
        var cells = new List<Cell>() { CellWithReference("A1"), CellWithReference("B1"), CellWithReference("D1"), CellWithReference("AB1") };

        var sut = new Row(cells);
        sut.ReindexTo(10);

        sut.RowIndex.Should().Be((uint)10);
    }

    [Fact]
    public void ReIndexRow_ShouldUpdateCellsRowIndex()
    {
        var cells = new List<Cell>() { CellWithReference("A1"), CellWithReference("B1"), CellWithReference("D1"), CellWithReference("AB1") };

        var sut = new Row(cells);
        sut.ReindexTo(7);

        var resultCells = sut.ChildElements.OfType<Cell>().ToArray();
        resultCells[0].CellReference!.Value.Should().Be("A7");
        resultCells[1].CellReference!.Value.Should().Be("B7");
        resultCells[2].CellReference!.Value.Should().Be("D7");
        resultCells[3].CellReference!.Value.Should().Be("AB7");
    }

    [InlineData("A1", 1)]
    [InlineData("Z31", 26)]
    [InlineData("AA1", 27)]
    [Theory]
    public void ColumnIndex_ShouldReturnCorrectColumnIndex(string reference, int expectedColumnIndex)
    {
        var sut = CellWithReference(reference);
        sut.CellReference!.GetIntegerColumnIndex().Should().Be(expectedColumnIndex);
    }

    private static Cell CellWithReference(string reference)
    {
        var cell = new Cell();
        cell.CellReference = reference;
        return cell;
    }
}