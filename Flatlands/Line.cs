using Flatlands.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flatlands
{
    public class Line
    {
        public Vector2 Begin { get; set; }
        public Vector2 End { get; set; }

        public Line() { }

        public Line(Vector2 begin, Vector2 end)
        {
            Begin = begin;
            End = end;
        }

        public Line(float beginPointX, float beginPointY, float endPointX, float endPointY)
        {
            Begin = new Vector2(beginPointX, beginPointY);
            End = new Vector2(endPointX, endPointY);
        }

        public bool Intersects(Line line)
        {
            return GetIntersectedPoint(line) != null;
        }

        public Vector2? GetIntersectedPoint(Line line)
        {
            float ua = (line.End.X - line.Begin.X) * 
                (Begin.Y - line.Begin.Y) - (line.End.Y - line.Begin.Y) * 
                (Begin.X - line.Begin.X);

            float ub = (End.X - Begin.X) * 
                (Begin.Y - line.Begin.Y) - (End.Y - Begin.Y) * 
                (Begin.X - line.Begin.X);

            float denominator = (line.End.Y - line.Begin.Y) * (End.X - Begin.X) -
                (line.End.X - line.Begin.X) * (End.Y - Begin.Y);
            
            if (Math.Abs(denominator) <= 0.00001f) // check if interlapses
            {
                if (Math.Abs(ua) <= 0.00001f && Math.Abs(ub) <= 0.00001f)
                    return Begin; // returns beggining of the line
            }
            else
            {
                ua /= denominator;
                ub /= denominator;

                if (ua >= 0 && ua <= 1 && ub >= 0 && ub <= 1)
                    return new Vector2(Begin.X + ua * (End.X - Begin.X),
                        Begin.Y + ua * (End.Y - Begin.Y));
            }

            return null;
        }

        public Vector2? GetIntersectedPoint(Entity entity, float lineAngleDegrees)
        {
            Vector2? intersectionFound = null;

            if (lineAngleDegrees > 0)
            {
                Line bottomLine = new Line(new Vector2(entity.BoundingBox.X, entity.BoundingBox.Bottom),
                    new Vector2(entity.BoundingBox.Right, entity.BoundingBox.Bottom));
                intersectionFound = GetIntersectedPoint(bottomLine);
            }
            else
            {
                Line topLine = new Line(new Vector2(entity.BoundingBox.X, entity.BoundingBox.Top),
                    new Vector2(entity.BoundingBox.Right, entity.BoundingBox.Top));
                intersectionFound = GetIntersectedPoint(topLine);
            }

            if (intersectionFound != null)
                return intersectionFound;

            if (lineAngleDegrees <= 90 && lineAngleDegrees > -90)
            {
                Line leftLine = new Line(new Vector2(entity.BoundingBox.Left, entity.BoundingBox.Y),
                    new Vector2(entity.BoundingBox.Left, entity.BoundingBox.Bottom));
                intersectionFound = GetIntersectedPoint(leftLine);
            }
            else
            {
                Line rightLine = new Line(new Vector2(entity.BoundingBox.Right, entity.BoundingBox.Y),
                    new Vector2(entity.BoundingBox.Right, entity.BoundingBox.Bottom));
                intersectionFound = GetIntersectedPoint(rightLine);
            }

            return intersectionFound;
        }

        public static Vector2? LineIntersectsLine(Vector2 point1, Vector2 point2, Vector2 point3, Vector2 point4)
        {
            float ua = (point4.X - point3.X) * (point1.Y - point3.Y) - (point4.Y - point3.Y) * (point1.X - point3.X);
            float ub = (point2.X - point1.X) * (point1.Y - point3.Y) - (point2.Y - point1.Y) * (point1.X - point3.X);
            float denominator = (point4.Y - point3.Y) * (point2.X - point1.X) -
                (point4.X - point3.X) * (point2.Y - point1.Y);

            // sacar se as bixa se sobrepõe
            if (Math.Abs(denominator) <= 0.00001f)
            {
                if (Math.Abs(ua) <= 0.00001f && Math.Abs(ub) <= 0.00001f)
                {
                    return point1; // retorna o comecinho da linha
                    //return (point1 + point2) / 2;
                }
            }
            else
            {
                ua /= denominator;
                ub /= denominator;

                if (ua >= 0 && ua <= 1 && ub >= 0 && ub <= 1)
                {
                    return new Vector2(point1.X + ua * (point2.X - point1.X),
                        point1.Y + ua * (point2.Y - point1.Y));
                }
            }

            return null;
        }
    }
}
