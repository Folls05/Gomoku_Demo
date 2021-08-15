using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gomoku_Demo
{
    class Game
    {
        private Board board = new Board();
        private PieceType currentPlayer = PieceType.BLACK;
        private PieceType winner = PieceType.None;
        public PieceType Winner { get { return winner; } }

        public bool CanBePlaced(int x, int y)
        {
            return board.CanBePlaced(x, y);
        }

        public Piece PlaceAPiece(int x, int y)
        {
            Piece piece = board.PlaceAPiece(x, y, currentPlayer);
            if (piece != null)
            {
                //檢查是否下棋的人獲勝
                CheckWinner();
                if (currentPlayer == PieceType.BLACK)
                {
                    currentPlayer = PieceType.WHITE;
                }
                else
                {
                    currentPlayer = PieceType.BLACK;
                }
                return piece;
            }
            return null;
        }

        private void CheckWinner()
        {
            int centerX = board.LastPlacedNode.X;
            int centerY = board.LastPlacedNode.Y;

            //檢查八個不同方向

            for (int xDir = -1; xDir <= 1; xDir++)
            {
                for (int yDir = -1; yDir <= 1; yDir++)
                {
                    //排除 X+0 Y+0 的情況
                    if (xDir ==0 && yDir ==0)
                    {
                        continue;
                    }

                    //紀錄現在看到幾顆相同的棋子
                    int count = 1;
                    int midCount = 0;
                    //起點去算五顆顏色是否一樣
                    while (count < 5)
                    {
                        int targetX = centerX + count * xDir;
                        int targetY = centerY + count * yDir;

                        //檢查顏色是否相同
                        if (targetX < 0 || targetX >= Board.NODE_COUNT ||
                             targetY < 0 || targetY >= Board.NODE_COUNT ||
                            board.GetPieceType(targetX, targetY) != currentPlayer)
                        {
                            break;
                        }
                        count++;
                    }
                    //由中間點去算是否五顆棋子一樣
                    while (midCount < 5)
                    {
                        int targetX = centerX - midCount * xDir;
                        int targetY = centerY - midCount * yDir;

                        if (targetX < 0 || targetX >= Board.NODE_COUNT ||
                        targetY < 0 || targetY >= Board.NODE_COUNT ||
                        board.GetPieceType(targetX, targetY) != currentPlayer)
                        {
                            break;
                        }
                        midCount++;
                    }
                    //檢查是否看到五顆棋子
                    if (count == 5)
                    {
                        winner = currentPlayer;
                    }
                    if (count + midCount > 5)
                    { 
                        winner = currentPlayer;
                    }
                }
            }


        }
    }
}
