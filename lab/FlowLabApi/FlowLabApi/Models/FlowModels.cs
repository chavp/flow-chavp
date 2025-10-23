using System.Text.Json;

namespace FlowLabApi.Models;

public static class FlowModels
{
    public record FlowData(
        Guid Id,
        DataSection Data,
        StateSection State,
        bool IsOptionsAPI
    );

    public record DataSection(
        List<Node> Nodes,
        List<Edge> Edges,
        List<double> Position,
        double Zoom,
        Viewport Viewport
    );

    public record StateSection(
        List<Node> Nodes,
        List<Edge> Edges,
        List<double> Position,
        double Zoom,
        Viewport Viewport
    );

    public record Node(
        Guid Id,
        string Type,
        bool Initialized,
        Position Position,
        NodeData Data
    );

    public record Position(
        double X,
        double Y
    );

    public record NodeData(
        string Label
    );

    public record Edge(
        Guid Id,
        string Type,
        string Source,
        string Target,
        EdgeData Data,
        string Label,
        bool Animated,
        double SourceX,
        double SourceY,
        double TargetX,
        double TargetY
    );

    public record EdgeData();

    public record Viewport(
        double X,
        double Y,
        double Zoom
    );

    public static void Test()
    {
        var json = """
        {
           "$id":"persistance-flow-001",
           "data": { ... }
        }
        """;

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        };

        var flowData = JsonSerializer.Deserialize<FlowData>(json, options);
    }
}
