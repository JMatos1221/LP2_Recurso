using System;
using System.Collections.Generic;

namespace LP2_Recurso
{
    /// <summary>
    /// Class Grid
    /// Creates the Grid with size
    /// </summary>
    public class Grid
    {
        ///  Array of pieces that are in the grid 
        private Piece[,] pieces;
        /// Grid Dimension
        int xDim, yDim;
        /// Random instance
        Random rnd;

        /// XDim Readonly Property 
        public int XDim => xDim;
        /// YDim Readonly Property
        public int YDim => yDim;
        /// List of positions that are updated 
        public List<int> PositionsToUpdate { get; set; }

        /// <summary>
        /// Grid constructor, creates a grid with width x and height y
        /// </summary>
        /// <param name="x">Grid Width</param>
        /// <param name="y">Grid Height</param>
        public Grid(int x, int y)
        {
            xDim = x;
            yDim = y;
            pieces = new Piece[yDim, xDim];
            rnd = new Random();
            PositionsToUpdate = new List<int>();
        }

        /// <summary>
        /// Generates random pieces and fills the grid  
        /// </summary>
        public void Fill()
        {
            int genPiece;

            /// For cycle to generate a piece type in every grid position 
            for (int i = 0; i < xDim; i++)
                for (int j = 0; j < yDim; j++)
                {
                    genPiece = rnd.Next(0, 4);

                    pieces[j, i] = (Piece)genPiece;
                }
        }

        /// <summary>
        /// Gets a random grid position and a Von Neumann's neighbour
        /// </summary>
        /// <param name="GetPositions("> Method Name</param>
        /// <returns>Returns a tuple of 4 ints, representing 2 grid positions
        ///  (x1, y1, x2, y2)</returns>
        private (int, int, int, int) GetPositions()
        {
            int x1, x2, y1, y2;

            x1 = rnd.Next(0, xDim);
            y1 = rnd.Next(0, yDim);

            x2 = x1;
            y2 = y1;

            /// Randomly picks if the neighbour will be vertical or horizontal
            /// and then randomly picks one of the two possible neighbours
            if (rnd.NextDouble() < 0.5)
            {
                x2 += rnd.NextDouble() < 0.5 ? -1 : 1;
            }
            else
            {
                y2 += rnd.NextDouble() < 0.5 ? -1 : 1;
            }

            /// <summary>
            /// If x is out of range, set it to the opposite side of the grid
            /// horizontally
            /// </summary>
            if (x2 < 0) x2 += xDim;
            else if (x2 == xDim) x2 -= xDim;

            /// If y is out of range, set it to the opposite side of the grid
            /// vertically
            if (y2 < 0) y2 += yDim;
            else if (y2 == yDim) y2 -= yDim;

            return (x1, y1, x2, y2);
        }


        /// <summary>
        /// Execute the Swap event
        /// </summary>
        public void Swap()
        {
            /// Gets the 2 coordinates [x,y]
            (int x1, int y1, int x2, int y2) = GetPositions();

            Piece pieceOne = pieces[y1, x1];
            Piece pieceTwo = pieces[y2, x2];

            /// Verify if there are different pieces
            if (pieceOne != pieceTwo)
            {
                Piece aux = pieces[y1, x1];
                /// Swap the pieces 
                SetPiece(x1, y1, pieceTwo);
                SetPiece(x2, y2, aux);

                /// Added the swapped pieces to the list 
                AddPositionsToUpdate(PositionsToUpdate, x1, y1, x2, y2);
            }
        }

        /// <summary>
        /// Execute the Reproduction event
        /// </summary>
        public void Reproduction()
        {
            /// Gets the 2 coordinates [x,y]
            (int x1, int y1, int x2, int y2) = GetPositions();

            Piece pieceOne = pieces[y1, x1];
            Piece pieceTwo = pieces[y2, x2];

            /// Verifies if the first coodinates are an empty cell
            if (pieceOne == Piece.None)
            {
                if (pieceTwo == Piece.None)
                    return;
                /// If one piece isn't empty and the other is
                /// The empty piece is set to the same color of the 
                /// other piece
                else
                {
                    SetPiece(x1, y1, pieceTwo);
                    /// Added the pieces to the list
                    AddPositionsToUpdate(PositionsToUpdate, x1, y1);
                }
            }
            else if (pieceTwo == Piece.None)
            {
                SetPiece(x2, y2, pieceOne);
                AddPositionsToUpdate(PositionsToUpdate, x2, y2);
            }
        }

        /// <summary>
        /// /// Execute the Selection event
        /// </summary>
        public void Selection()
        {
            /// Gets the 2 coordinates [x,y]
            (int x1, int y1, int x2, int y2) = GetPositions();

            Piece pieceOne = pieces[y1, x1];
            Piece pieceTwo = pieces[y2, x2];

            /// If one of the selected pieces is empty or they are both the same,
            /// nothing happens
            if (pieceOne == Piece.None || pieceTwo == Piece.None ||
            pieceOne == pieceTwo)
                return;

            /// Conditions for piece one to win
            if ((pieceOne == Piece.Rock && pieceTwo == Piece.Scissors) ||
            (pieceOne == Piece.Paper && pieceTwo == Piece.Rock) ||
            (pieceOne == Piece.Scissors && pieceTwo == Piece.Paper))
            {
                SetPiece(x2, y2, Piece.None);
                AddPositionsToUpdate(PositionsToUpdate, x2, y2);
            }
            /// If piece one didn't win, piece two wins
            else
            {
                SetPiece(x1, y1, Piece.None);
                AddPositionsToUpdate(PositionsToUpdate, x1, y1);
            }
        }

        /// <summary>
        /// Sets the piece in the given position to the given type
        /// </summary>
        /// /// <param name="x">Grid width position</param>
        /// <param name="y">Grid height position</param>
        /// <param name="piece">Piece type</param>
        private void SetPiece(int x, int y, Piece piece)
        {
            pieces[y, x] = piece;
        }

        /// <summary>
        /// Gets the piece type in the given position
        /// </summary>
        /// <param name="x">Grid width position</param>
        /// <param name="y">Grid height position</param>
        /// <returns>Piece type</returns>
        public Piece GetPiece(int x, int y)
        {
            return pieces[y, x];
        }

        /// <summary>
        /// Adds grid positions to the PositionsToUpdate list
        /// </summary>
        /// <param name="list">List to add positons to</param>
        /// <param name="toAdd">Positions to add to the list of
        /// positions to update</param>
        private void AddPositionsToUpdate(List<int> list, params int[] toAdd)
        {
            foreach (int i in toAdd)
                list.Add(i);
        }
    }
}