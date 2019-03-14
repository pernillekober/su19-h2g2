namespace Galaga_Exercise__1 {
    public class Class1 {
        private int score;
        private Text display;
        
        public Score(Vec2F position, Vec2F extent) {
            score = 0;
            display = new Text(score.ToString(), position, extent);
        }
        public void AddPoint(...) {
            ...
        }
        public void RenderScore() {
            display.SetText(string.Format("Score: {0}", score.ToString()));
            display.SetColor(new Vec3I(255, 0, 0));
            display.RenderText();

    }
}