using System;

namespace Battleship
{
    public class AniTheGamer : BattleshipAgent
    {
        private string name;
        private char[,] board;
        private int x;
        private int y;
        private Random rng;

        public AniTheGamer()
        {
            this.name = "Ani The cool Gamer";
            this.rng = new Random();
        }

        public override void Initialize()
        {
            this.board = new char[10, 10];
            this.x = this.rng.Next(10);
            this.y = this.rng.Next(10);
        }

        public override string GetNickname() => this.name;

        public override void SetOpponent(string opponent)
        {
        }

        public override GridSquare LaunchAttack()
        {
            for (; this.board[this.x, this.y] > char.MinValue; this.y = this.rng.Next(10))
                this.x = this.rng.Next(10);
            return new GridSquare(this.x, this.y);
        }

        public override void DamageReport(char report)
        {
            if (report == char.MinValue)
                this.board[this.x, this.y] = 'X';
            else
                this.board[this.x, this.y] = report;
        }

        public override BattleshipFleet PositionFleet() => new BattleshipFleet()
        {
            PatrolBoat = new ShipPosition(this.rng.Next(10), rotation: 'V'),
            Submarine = new ShipPosition(this.rng.Next(8), 3),
            Destroyer = new ShipPosition(this.rng.Next(8), 4),
            Battleship = new ShipPosition(this.rng.Next(7), 6, rotation:'V'),
            Carrier = new ShipPosition(7 + this.rng.Next(3), 5, rotation:'V')
        };
    }
}
