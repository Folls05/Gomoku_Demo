using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Gomoku_Demo
{
    class Board
    {
        public static readonly int NODE_COUNT = 9;
        private static readonly Point NO_MATCH_NODE = new Point(-1, -1);

        private static readonly int OFFSET = 75;
        private static readonly int NODE_RADIUS = 10;
        private static readonly int NODE_DISTANCE = 75;

        private Piece[,] pieces = new Piece[NODE_COUNT, NODE_COUNT];

        private Point lastPlacedNode = NO_MATCH_NODE;
        public Point LastPlacedNode { get { return lastPlacedNode; } }
        public bool CanBePlaced(int x, int y)
        {
            Point nodeId = FindTheCloserNode(x, y);

            if (nodeId == NO_MATCH_NODE)
                return false;

            if (pieces[nodeId.X, nodeId.Y] != null)
            {
                return false;
            }

            return true;
        }

        private Point FindTheCloserNode(int x, int y)
        {
            int nodeIdX = FindTheCloserNode(x);
            if (nodeIdX == -1 || nodeIdX >= NODE_COUNT)
                return NO_MATCH_NODE;
            int nodeIdY = FindTheCloserNode(y);
            if (nodeIdY == -1 || nodeIdY >= NODE_COUNT)
                return NO_MATCH_NODE;
            return new Point(nodeIdX, nodeIdY);
        }
        private int FindTheCloserNode(int pos)
        {
            if (pos < OFFSET - NODE_RADIUS)
                return -1;
            pos -= OFFSET;


            int quotient = pos / NODE_DISTANCE;
            int remainder = pos % NODE_DISTANCE;

            if (remainder <= NODE_RADIUS)
                return quotient;
            else if (remainder >= NODE_DISTANCE - NODE_RADIUS)
                return quotient + 1;
            else
                return -1;



        }

        public Piece PlaceAPiece(int x, int y, PieceType type)
        {
            //找出最近的節點
            Point nodeId = FindTheCloserNode(x, y);

            //如果沒有的話 回傳NULL
            if (nodeId == NO_MATCH_NODE)
                return null;

            //如果有的話 檢查是否有棋子
            if (pieces[nodeId.X, nodeId.Y] != null)
            {
                return null;
            }

            //根據type 產生對應棋子
            Point formPos = convertToFormPostion(nodeId);

            if (type == PieceType.BLACK)
            {
                pieces[nodeId.X, nodeId.Y] = new BlackPiece(formPos.X, formPos.Y);
            }
            else if (type == PieceType.WHITE)
            {
                pieces[nodeId.X, nodeId.Y] = new WhitePiece(formPos.X, formPos.Y);
            }
            else
            {
                return null;
            }

            //紀錄最後下棋子的位置
            lastPlacedNode = nodeId;
            return pieces[nodeId.X, nodeId.Y];


        }

        private Point convertToFormPostion(Point nodeId)
        {
            Point formPosition = new Point();
            formPosition.X = nodeId.X * NODE_DISTANCE + OFFSET;
            formPosition.Y = nodeId.Y * NODE_DISTANCE + OFFSET;
            return formPosition;
        }

        //知道點上是甚麼顏色
        public PieceType GetPieceType(int nodeIDx, int nodeIdY)
        {
            if (pieces[nodeIDx, nodeIdY] == null)
            {
                return PieceType.None;
            }
            else
            {

                return pieces[nodeIDx, nodeIdY].GetPieceType();
            }
        }
    }
}


