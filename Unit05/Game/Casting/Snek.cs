using System;
using System.Collections.Generic;
using System.Linq;

namespace Unit05.Game.Casting
{
    /// <summary>
    /// <para>A long limbless reptile.</para>
    /// <para>The responsibility of Snake is to move itself.</para>
    /// </summary>
    public class Snek : Actor
    {
        private List<Actor> segmentz = new List<Actor>();

        /// <summary>
        /// Constructs a new instance of a Snake.
        /// </summary>
        public Snek()
        {
            PrepareBody();
        }

        /// <summary>
        /// Gets the snake's body segmentz.
        /// </summary>
        /// <returns>The body segmentz in a List.</returns>
        public List<Actor> GetBody()
        {
            return new List<Actor>(segmentz.Skip(1).ToArray());
        }

        /// <summary>
        /// Gets the snake's head segment.
        /// </summary>
        /// <returns>The head segment as an instance of Actor.</returns>
        public Actor GetHead()
        {
            return segmentz[0];
        }

        /// <summary>
        /// Gets the snake's segmentz (including the head).
        /// </summary>
        /// <returns>A list of snake segmentz as instances of Actors.</returns>
        public List<Actor> GetSegmentz()
        {
            return segmentz;
        }

        /// <summary>
        /// Grows the snake's tail by the given number of segmentz.
        /// </summary>
        /// <param name="numberOfSegmentz">The number of segmentz to grow.</param>
        public void GrowTail(int numberOfSegmentz)
        {
            for (int i = 0; i < numberOfSegmentz; i++)
            {
                Actor tail = segmentz.Last<Actor>();
                Point velocity = tail.GetVelocity();
                Point offset = velocity.Reverse();
                Point position = tail.GetPosition().Add(offset);

                Actor segment = new Actor();
                segment.SetPosition(position);
                segment.SetVelocity(velocity);
                segment.SetText("#");
                segment.SetColor(Constants.GREEN);
                segmentz.Add(segment);
            }
        }

        /// <inheritdoc/>
        public override void MoveNext()
        {
            foreach (Actor segment in segmentz)
            {
                segment.MoveNext();
            }

            for (int i = segmentz.Count - 1; i > 0; i--)
            {
                Actor trailing = segmentz[i];
                Actor previous = segmentz[i - 1];
                Point velocity = previous.GetVelocity();
                trailing.SetVelocity(velocity);
            }
        }

        /// <summary>
        /// Turns the head of the snake in the given direction.
        /// </summary>
        /// <param name="velocity">The given direction.</param>
        public void TurnHead(Point directions)
        {
            segmentz[0].SetVelocity(directions);
        }

        /// <summary>
        /// Prepares the snake body for moving.
        /// </summary>
        private void PrepareBody()
        {
            int x = Constants.MAX_X / 2;
            int y = Constants.MAX_Y / 2;

            for (int i = 0; i < Constants.SNAKE_LENGTH; i++)
            {
                Point position = new Point(x - i * Constants.CELL_SIZE, y);
                Point velocity = new Point(1 * Constants.CELL_SIZE, 0);
                string text = i == 0 ? "8" : "#";
                Color color = i == 0 ? Constants.YELLOW : Constants.GREEN;
                // Point start = new Point(15, 20);

                Actor segment = new Actor();
                segment.SetPosition(position);
                segment.SetVelocity(velocity);
                segment.SetText(text);
                segment.SetColor(color);
                segmentz.Add(segment);
            }
        }
    }
}