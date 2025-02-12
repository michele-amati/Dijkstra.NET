using System;
using System.Collections.Generic;
using Dijkstra.NET.Graph;

namespace Dijkstra.NET.ShortestPath
{
    internal static class Dijkstra
    {
        public static Dictionary<uint, Dictionary<string, object?>> GetDistancesFromDestination(IDijkstraGraph graph, uint to, int depth)
        {
            var distance = new Dictionary<uint, Dictionary<string, object?>>();
            List<uint> q = new List<uint>();
            var d = new Dictionary<uint, int> { [to] = 0 };
            var current = new HashSet<uint>();

            Dictionary<string, object?> innerDict = new Dictionary<string, object?>();
            innerDict.Add("parent", null);
            innerDict.Add("distance", 0);

            distance.Add(to, innerDict);

            q.Add(to);

            int Distance(uint key)
            {
                return distance.ContainsKey(key) ? (int)distance[key]["distance"] : Int32.MaxValue;
            }

            do
            {
                uint u = q[0];
                q.RemoveAt(0);

                if (depth == d[u])
                {
                    continue;
                }

                graph[u]((node, cost) =>
                {
                    if (Distance(node) > Distance(u) + cost)
                    {
                        if (current.Contains(node))
                        {
                            q.Remove(node);
                        }

                        var distanceDict = new Dictionary<string, object> { ["parent"] = u, ["distance"] = Distance(u) + cost };
                        distance[node] = distanceDict;

                        q.Add(node);
                        d[node] = d[u] + 1;
                    }
                });
            } while (q.Count > 0);

            return distance;
        }

        public static ShortestPathResult GetShortestPath(IDijkstraGraph graph, uint from, uint to, int depth)
        {
            var path = new Dictionary<uint, uint>();
            var distance = new Dictionary<uint, int> {[from] = 0};
            var d = new Dictionary<uint, int> {[from] = 0};
            var q = new SortedSet<uint>(new[] {from}, new NodeComparer(distance));
            var current = new HashSet<uint>();

            int Distance(uint key)
            {
                return distance.ContainsKey(key) ? distance[key] : Int32.MaxValue;
            }

            do
            {
                uint u = q.Deque();

                if (u == to)
                {
                    return new ShortestPathResult(from, to, distance[u], path);
                }

                current.Remove(u);

                if (depth == d[u])
                {
                    continue;
                }

                graph[u]((node, cost) =>
                {
                    if (Distance(node) > Distance(u) + cost)
                    {
                        if (current.Contains(node))
                        {
                            q.Remove(node);
                        }

                        distance[node] = Distance(u) + cost;
                        q.Add(node);
                        current.Add(node);
                        path[node] = u;
                        d[node] = d[u] + 1;
                    }
                });

            } while (q.Count > 0 && depth > 0);

            return new ShortestPathResult(from, to);
        }
    }
}