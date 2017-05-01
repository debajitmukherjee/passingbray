using PassingBray.Model.Enums;


namespace PassingBray.Model
{
    public class Card
    {
        public int Number { get; set; }

        public string Image { get; set; }

        public CardType CardType { get; set; }

        public bool IsReceived { get; set; }
    }
}
