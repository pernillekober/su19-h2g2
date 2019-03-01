
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga_Exercise__1 {
    public class Score {
        private int score;
        private Text display;
        public Score(Vec2F position, Vec2F extent) {
            score = 0;
            display = new Text(score.ToString(), position, extent);
        }
        /// <summary>
        /// Increases the score with one point.
        /// </summary>
        public void AddPoint() {
            score++;
        }
        /// <summary>
        /// Adds a score display to the game.
        /// </summary>
        public void RenderScore() {
            display.SetText(string.Format("Score: {0}", score.ToString()));
            display.SetColor(new Vec3I(255, 0, 0));
            display.RenderText();
        }
    }
}