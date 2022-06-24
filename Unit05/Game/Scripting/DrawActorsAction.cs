using System.Collections.Generic;
using Unit05.Game.Casting;
using Unit05.Game.Services;


namespace Unit05.Game.Scripting
{
    /// <summary>
    /// <para>An output action that draws all the actors.</para>
    /// <para>The responsibility of DrawActorsAction is to draw each of the actors.</para>
    /// </summary>
    public class DrawActorsAction : Action
    {
        private VideoService videoService;

        /// <summary>
        /// Constructs a new instance of ControlActorsAction using the given KeyboardService.
        /// </summary>
        public DrawActorsAction(VideoService videoService)
        {
            this.videoService = videoService;
        }

        /// <inheritdoc/>
        public void Execute(Cast cast, Script script)
        {

            Snake snake = (Snake)cast.GetFirstActor("snake");
            Snek snek = (Snek)cast.GetFirstActor("snek");
            List<Actor> segments = snake.GetSegments();
            List<Actor> segmentz = snek.GetSegmentz();
            Actor score = cast.GetFirstActor("score");
            Score scores = (Score)cast.GetFirstActor("score");
            int points = 1;
                scores.AddPoints(points);
                
            snake.GrowTail(points, true);
            snek.GrowTail(points, true);
           
            List<Actor> messages = cast.GetActors("messages");
            
            videoService.ClearBuffer();
            videoService.DrawActors(segments);
            videoService.DrawActors(segmentz);
            videoService.DrawActor(score);
            
            videoService.DrawActors(messages);
            videoService.FlushBuffer();

        }
    }
}