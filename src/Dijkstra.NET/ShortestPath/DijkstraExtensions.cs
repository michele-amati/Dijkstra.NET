using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dijkstra.NET.Graph;

namespace Dijkstra.NET.ShortestPath
{
    public static class DijkstraExtensions
    {
        /// <summary>
        /// Get path from @from to @to
        /// </summary>
        /// <param name="graph">Source graph</param>
        /// <param name="from">Start node</param>
        /// <param name="to">End node</param>
        /// <returns>Value with path</returns>
        public static ShortestPathResult Dijkstra(this IDijkstraGraph graph, uint from, uint to)
            => Dijkstra(graph, from, to, Int32.MaxValue);

        /// <summary>
        /// Get path and distancies from the destination node
        /// </summary>
        /// <param name="graph">Source graph</param>
        /// <param name="to">Destination node</param>
        /// <returns>Return a dictionary with every node reacheble from the destination node</returns>
        public static Dictionary<uint, Dictionary<string, object?>> DijkstraPathFromDestination(this IDijkstraGraph graph, uint to)
            => DijkstraPathFromDestination(graph, to, Int32.MaxValue);

        /// <summary>
        /// Get path from @from to @to
        /// </summary>
        /// <param name="graph">Source graph</param>
        /// <param name="from">Start node</param>
        /// <param name="to">End node</param>
        /// <param name="depth">Depth of path</param>
        /// <returns>Value with path</returns>
        public static ShortestPathResult Dijkstra(this IDijkstraGraph graph, uint from, uint to, int depth)
        {
            return ShortestPath.Dijkstra.GetShortestPath(graph, from, to, depth);
        }

        /// <summary>
        /// Get path and distancies from the destination node
        /// </summary>
        /// <param name="graph">Source graph</param>
        /// <param name="to">Destination node</param>
        /// <param name="depth">Depth of path</param>
        /// <returns>Return a dictionary with every node reacheble from the destination node</returns>
        public static Dictionary<uint, Dictionary<string, object?>> DijkstraPathFromDestination(this IDijkstraGraph graph, uint to, int depth)
        {
            return ShortestPath.Dijkstra.GetDistancesFromDestination(graph, to, depth);
        }
    }
}