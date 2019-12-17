using HexCardGame.SharedData;
using UnityEngine;

namespace HexCardGame.Runtime.GameBoard
{
    public interface IBoard
    {
        Position[] Positions { get; }
        bool HasPosition(int x, int y);
        Position? GetPosition(int x, int y);
        Position? GetPosition(Vector3Int position);
        void GeneratePositions();
    }

    public class Board : IBoard
    {
        public Board(BoardController controller, BoardData data)
        {
            Data = data;
            Controller = controller;
            GeneratePositions();
        }

        BoardData Data { get; }
        BoardController Controller { get; }
        public Position[] Positions { get; private set; }

        public void GeneratePositions()
        {
            var positions = Data.GetHexPositions();
            Positions = new Position[positions.Length];
            for (var index = 0; index < positions.Length; index++)
            {
                var i = positions[index];
                Positions[index] = new Position(i);
            }

            OnCreateBoard();
        }

        public bool HasPosition(int x, int y) => GetPosition(x, y) != null;
        public Position? GetPosition(Vector3Int p) => GetPosition(p.x, p.y);

        public Position? GetPosition(int x, int y)
        {
            foreach (var i in Positions)
            {
                if (i.Hex.x != x)
                    continue;
                if (i.Hex.y == y)
                    return i;
            }

            return null;
        }

        void OnCreateBoard() => Controller.HandleCreateBoard(this);
    }
}