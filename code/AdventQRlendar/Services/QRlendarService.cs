using System.Diagnostics;

namespace AdventQRlendar.Services;

public sealed class QRlendarService
{
    private static readonly int columns = 4;
    private static readonly int rows = 6;

    private static readonly string[] wikipedia = new[]
    {
        "https://en.wikipedia.org/wiki/1",
        "https://en.wikipedia.org/wiki/2",
        "https://en.wikipedia.org/wiki/3",
        "https://en.wikipedia.org/wiki/4",
        "https://en.wikipedia.org/wiki/5",
        "https://en.wikipedia.org/wiki/6",
        "https://en.wikipedia.org/wiki/7",
        "https://en.wikipedia.org/wiki/8",
        "https://en.wikipedia.org/wiki/9",
        "https://en.wikipedia.org/wiki/10",

        "https://en.wikipedia.org/wiki/11_(number)",
        "https://en.wikipedia.org/wiki/12_(number)",
        "https://en.wikipedia.org/wiki/13_(number)",
        "https://en.wikipedia.org/wiki/14_(number)",
        "https://en.wikipedia.org/wiki/15_(number)",
        "https://en.wikipedia.org/wiki/16_(number)",
        "https://en.wikipedia.org/wiki/17_(number)",
        "https://en.wikipedia.org/wiki/18_(number)",
        "https://en.wikipedia.org/wiki/19_(number)",
        "https://en.wikipedia.org/wiki/20_(number)",

        "https://en.wikipedia.org/wiki/21_(number)",
        "https://en.wikipedia.org/wiki/22_(number)",
        "https://en.wikipedia.org/wiki/23_(number)",
        "https://en.wikipedia.org/wiki/24_(number)",
    };

    public int NumberOfDoors { get; } = columns * rows;

    private readonly Door[] doors;
    private int[] indices;

    public QRlendarService()
    {
        doors = Enumerable.Range(1, NumberOfDoors)
            .Select(static i => new Door(i))
            .ToArray();

        indices = Enumerable.Range(0, NumberOfDoors)
            .OrderBy(static number => Random.Shared.Next())
            .ToArray();

        Doors = doors;
    }

    public IEnumerable<Door> Doors { get; }

    public IEnumerable<IEnumerable<Door>> GetGrid()
    {
        int index = 0;
        for (int row = 0; row < rows; row++)
        {
            yield return GetRow(row);

        }

        IEnumerable<Door> GetRow(int row)
        {
            for (int column = 0; column < columns; column++)
            {
                int doorIndex = indices[index++];
                yield return doors[doorIndex];
            }
        }
    }

    public void Shuffle()
    {
        indices = Enumerable.Range(0, NumberOfDoors)
            .OrderBy(static number => Random.Shared.Next())
            .ToArray();
    }

    public void Fill()
    {
        Debug.Assert(doors.Length == wikipedia.Length);

        for (int i = 0; i < doors.Length; i++)
        {
            Door door = doors[i];

            door.Url = wikipedia[i];
        }
    }
}
