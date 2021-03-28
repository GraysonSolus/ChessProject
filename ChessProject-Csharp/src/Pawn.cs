﻿using SolarWinds.MSP.Chess.Core.Interfaces;
using src.Core.BaseImplementations;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace SolarWinds.MSP.Chess
{
    public class Pawn : ChessPieceBase
    {
        private bool m_isFirstMove = true;

        // These rules are tied to the chess board size, but have no reference to it. Consider refactoring into more generic way of setting initial coordinates
        private List<Point> m_validWhiteStartCoordinates = new List<Point>
        {
            new Point(0, 1),
            new Point(1, 1),
            new Point(2, 1),
            new Point(3, 1),
            new Point(4, 1),
            new Point(5, 1),
            new Point(6, 1),
            new Point(7, 1)
        };

        // These rules are tied to the chess board size, but have no reference to it. Consider refactoring into more generic way of setting initial coordinates
        private List<Point> m_validBlackStartCoordinates = new List<Point> 
        {
            new Point(0, 6),
            new Point(1, 6),
            new Point(2, 6),
            new Point(3, 6),
            new Point(4, 6),
            new Point(5, 6),
            new Point(6, 6),
            new Point(7, 6)
        };

        public Pawn(PieceColor pieceColor)
        {
            PieceColor = pieceColor;
            ValidBlackStartPositions = m_validBlackStartCoordinates;
            ValidWhiteStartPositions = m_validWhiteStartCoordinates;
        }

        public override bool IsValidMove(int xCoordinate, int yCoordinate)
        {
            // TODO: Consider breaking this out in to the PawnMoveValidator, and injecting it into the base class
            // Pawn is potentially doing too much here;
            if (XCoordinate != xCoordinate)
                return false;

            return PieceColor == PieceColor.Black ? ValidateBlackPieceMove(xCoordinate, yCoordinate) : ValidateWhitePieceMove(xCoordinate, yCoordinate);
        }

        // Not a fan of this method being inside the chess piece itself. Having to validate the move in here, means we're less able to make the move logic generic.
        // Would prefer to have separate move engine, which can handle movement of pieces in a more generic manner, called from the chess board instead.
        public override void Move(MovementType movementType, int newX, int newY)
        {
            switch (movementType)
            {
                case MovementType.Move:
                    {
                        if (IsValidMove(newX, newY))
                        {
                            XCoordinate = newX;
                            YCoordinate = newY;

                            m_isFirstMove = false;
                        }

                        break;
                    }
                default:
                    {
                        throw new NotImplementedException("Need to implement Pawn.Move()");
                    }
            }
        }

        private bool ValidateBlackPieceMove(int xCoordinate, int yCoordinate)
        {
            if (yCoordinate > YCoordinate)
                return false;

            if (m_isFirstMove)
            {
                if (YCoordinate - yCoordinate > 2)
                    return false;

                return true;
            }
            else
            {
                if (YCoordinate - yCoordinate > 1)
                    return false;

                return true;
            }
        }

        private bool ValidateWhitePieceMove(int xCoordinate, int yCoordinate)
        {
            if (YCoordinate > yCoordinate)
                return false;

            if (m_isFirstMove)
            {
                if (yCoordinate - YCoordinate > 2)
                    return false;

                return true;
            }
            else
            {
                if (yCoordinate - YCoordinate > 1)
                    return false;

                return true;
            }
        }

    }
}
