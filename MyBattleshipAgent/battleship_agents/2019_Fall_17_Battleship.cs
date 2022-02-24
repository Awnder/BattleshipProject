using System;
using System.Collections.Generic;


namespace Battleship
{
    //#*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*#
    public struct game_board_cell
    {
        public int pos_x { get; set; }
        public int pos_y { get; set; }
        public game_board_cell(int x = 0, int y = 0)
        {
            pos_x = x;
            pos_y = y;
        }
    }
    //#*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*#
    public struct battleship_vector
    {
        public char rotation { get; set; }
        public int start_pos_x { get; set; }
        public int start_pos_y { get; set; }
        public int Length { get; set; }
        private const int V = 0;
        public List<game_board_cell> ship_location_cells;

        //ref: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/how-to-define-value-equality-for-a-type
        //****************************************************************************************************************************
        public override bool Equals(Object battleship_vector_object)
        {
            return battleship_vector_object is battleship_vector && this == (battleship_vector)battleship_vector_object;
        }
        //****************************************************************************************************************************
        public static bool operator ==(battleship_vector vector_x, battleship_vector vector_y)
        {
            bool hit_in_vector_path = false;
            bool hit_in_start_pos = false;

            if (vector_x.start_pos_x == vector_y.start_pos_x && vector_x.start_pos_y == vector_y.start_pos_y && vector_x.rotation == vector_y.rotation)
            {
                hit_in_start_pos = true;
            }

            foreach (game_board_cell next_cell_x in vector_x.ship_location_cells)
            {
                if (hit_in_vector_path == true)
                {
                    break;
                }
                foreach (game_board_cell next_cell_y in vector_y.ship_location_cells)
                {
                    if (next_cell_x.pos_x == next_cell_y.pos_x && next_cell_x.pos_y == next_cell_y.pos_y)
                    {
                        
                        return (true);
                    }
                }
            }

            if (hit_in_start_pos == true)
            {
                return (true);
            }

            return (false);
        }
        //****************************************************************************************************************************
        public override int GetHashCode()
        {
            return start_pos_x * ((double)V).GetHashCode() + start_pos_y;
        }
        //****************************************************************************************************************************
        public static bool operator !=(battleship_vector vector_x, battleship_vector vector_y)
        {
            return !(vector_x == vector_y);
        }
        //****************************************************************************************************************************
        public battleship_vector(char orientation, int x, int y, int length)
        {
            rotation = orientation;
            start_pos_x = x;
            start_pos_y = y;
            Length = length;
            ship_location_cells = new List<game_board_cell>();
            int vector_index = 0;

            if (orientation == ShipRotation.Horizontal)
            {
                vector_index = start_pos_x;
            }
            else
            {
                vector_index = start_pos_y;
            }

            if (Length > 0)
            {

                ship_location_cells.Add(new game_board_cell(start_pos_x, start_pos_y));
                vector_index++;

                for (int CellIndex = 1; CellIndex < Length; CellIndex++, vector_index++)
                {

                    if (rotation == ShipRotation.Horizontal)
                    {
                        ship_location_cells.Add(new game_board_cell(vector_index, start_pos_y));

                    }
                    else if (rotation == ShipRotation.Vertical)
                    {

                        ship_location_cells.Add(new game_board_cell(start_pos_x, vector_index));

                    }

                }
            }
        }
    }
    //#*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*#
    public struct battle_ship
    {
        public char classification { get; set; }
        public battleship_vector position { get; set; }

        //****************************************************************************************************************************
        public ShipPosition GetPosition
        {
            get
            {
                ShipPosition ThisPosition = new ShipPosition(position.start_pos_x, position.start_pos_y, position.rotation);
                return (ThisPosition);
            }
        }
        //****************************************************************************************************************************
        public battle_ship(char Ship, int pos_x, int pos_y, char Orientation)
        {
            classification = Ship;
            int Length = 0;

            switch (char.ToUpper(Ship))
            {
                case ShipType.Carrier:
                    Length = 5;
                    break;
                case ShipType.Battleship:
                    Length = 4;
                    break;
                case ShipType.Destroyer:
                    Length = 3;
                    break;
                case ShipType.Submarine:
                    Length = 3;
                    break;
                case ShipType.PatrolBoat:
                    Length = 2;
                    break;
                default:
                    Length = 0;
                    break;
            }

            position = new battleship_vector(Orientation, pos_x, pos_y, Length);
        }
    }
    //#*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*#
    public enum attack_direction
    {
        None,
        Right,
        Down
    }
    //#*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*#
    public enum attack_surface_mode
    {
        hunt_mode,
        attack_mode
    }
    //#*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*#
    public struct AttackSurface
    {
        public void Initialize(int lengthX = 10, int LengthY = 10)
        {
            bound_x_max = lengthX;
            bound_y_max = lengthX;
            Mode = attack_surface_mode.hunt_mode;
            number_of_attack_cycles = 0;
            direction = attack_direction.None;
            hit_pos_x = 0;
            hit_pos_y = 0;
            next_attack_move_x = 0;
            next_attack_move_y = 0;
            AttackHistory = new char[bound_x_max, bound_y_max];
            opponent_battle_group = new Dictionary<char, ShipPosition>();
            opponent_sunken_battle_group = new Dictionary<char, ShipPosition>();
        }
        //****************************************************************************************************************************
        public void UpdateAttackSurface(char Result)
        {
            if (Result == ShipType.None && Mode == attack_surface_mode.hunt_mode)
            {
                //Miss update X,Y
                UpdateHuntModeCoordinates();
            }
            else 
            {
                //We have a hit
                UpdateAttackModeCoordinates(Result);

            }
        }
        //****************************************************************************************************************************
        public void GetNextAttachCoordinates(ref GridSquare Move)
        {
            if (Mode == attack_surface_mode.hunt_mode)
            {
                Move.x = master_position_x;
                Move.y = master_position_y;
            }
            else if (Mode == attack_surface_mode.attack_mode)
            {
                Move.x = next_attack_move_x;
                Move.y = next_attack_move_y;
            }
            else
            {
                Move.x = 0;
                Move.y = 0;
            }
        }
        //****************************************************************************************************************************
        private void UpdateHuntModeCoordinates()
        {
            if (master_position_x <= (bound_x_max - 1))
            {
                master_position_x++;
            }

            if (master_position_x > (bound_x_max - 1))
            {
                //Reached the right edge reset  X
                master_position_x = 0;
                master_position_y++;

                if (master_position_y > (bound_y_max - 1))
                {
                    //Reached the bottom edge reset  Y
                    master_position_y = 0;
                }
            }
        }
        //****************************************************************************************************************************
        private void UpdateAttackModeCoordinates(char Result)
        {

            if (HaveSeenShip(Result) == true)
            {
                if (Mode == attack_surface_mode.attack_mode)
                {
                    if (opponent_battle_group.ContainsKey(Result) == false)
                    {
                        Result = ShipType.None;
                    }
                }
                else
                {
                    UpdateMasterCoordinates(Result, master_position_x, master_position_y, 'V');
                    ResetAttack();
                    return;

                }
            }

            switch (direction)
            {
                case attack_direction.None:
                    //start of attack, save attack coordinates
                    Mode = attack_surface_mode.attack_mode;
                    hit_pos_x = master_position_x;
                    hit_pos_y = master_position_y;
                    SaveHit(Result, hit_pos_x, hit_pos_y);

                    //set our attack direction ot right 
                    direction = attack_direction.Right;

                    next_attack_move_x = hit_pos_x + 1;
                    next_attack_move_y = hit_pos_y;
                    if (next_attack_move_x > (bound_x_max - 1))
                    {
                        //Hit the right edge
                        next_attack_move_x = hit_pos_x;
                        //Update downward move
                        direction = attack_direction.Down;
                        next_attack_move_y = hit_pos_y + 1;

                        //check we we gone past the edge
                        if (next_attack_move_y > (bound_y_max - 1))
                        {
                            ResetAttack();
                            break;
                        }
                    }
                    //First hit no hit-test needed
                    number_of_attack_cycles++;
                    break;
                case attack_direction.Right:
                    if (Result != ShipType.None)
                    {
                        //update position
                        next_attack_move_x++;
                        //Check if we have gone past the edge
                        if (next_attack_move_x > (bound_x_max - 1))
                        {
                            //at the right edge
                            //check if we hit a small ship
                            if (number_of_attack_cycles >= min_ship_length)
                            {
                                if (HitTest(Result, number_of_attack_cycles) == true)
                                {
                                    //Found a ship and its sunk
                                    SaveSunkenShip(Result, hit_pos_x, hit_pos_y, 'H');
                                    UpdateMasterCoordinates(Result, hit_pos_x, hit_pos_y, 'H');
                                    ResetAttack();
                                    break;
                                }
                            }
                            else
                            {
                                //no ship on position + 1, reset and try down direction
                                next_attack_move_x = hit_pos_x;
                                //look down
                                next_attack_move_y = hit_pos_y + 1;
                                direction = attack_direction.Down;

                                //check we we gone past the edge
                                if (next_attack_move_y > (bound_y_max - 1))
                                {
                                    //check if we hit a small ship
                                    if (number_of_attack_cycles >= min_ship_length)
                                    {
                                        if (HitTest(Result, number_of_attack_cycles) == true)
                                        {
                                            //Found a ship and sunk it
                                            SaveSunkenShip(Result, hit_pos_x, hit_pos_y, 'H');
                                            UpdateMasterCoordinates(Result, hit_pos_x, hit_pos_y, 'H');
                                        }
                                    }
                                    ResetAttack();
                                    break;
                                }
                            }
                        }

                        number_of_attack_cycles++;
                        //check if we sunk a skip? 
                        if (HitTest(Result, number_of_attack_cycles) == true)
                        {
                            SaveSunkenShip(Result, hit_pos_x, hit_pos_y, 'H');
                            UpdateMasterCoordinates(Result, hit_pos_x, hit_pos_y, 'H');
                            ResetAttack();
                        }
                    }
                    else
                    { //miss in the right direction                   
                      //check if we hit a small ship
                        if (number_of_attack_cycles >= min_ship_length)
                        {
                            if (HitTest(Result, number_of_attack_cycles) == true)
                            {
                                //Found a ship and sunk it
                                SaveSunkenShip(Result, hit_pos_x, hit_pos_y, 'H');
                                UpdateMasterCoordinates(Result, hit_pos_x, hit_pos_y, 'H');
                            }
                            ResetAttack();
                        }
                        else
                        {
                            //no ship on position + 1, reset and try down direction 
                            next_attack_move_x = hit_pos_x;
                            //look down
                            direction = attack_direction.Down;
                            next_attack_move_y = hit_pos_y + 1;
                            if (next_attack_move_y > (bound_y_max - 1))
                            {
                                //Something is wrong only one hit in x,x
                                ResetAttack();
                                break;
                            }
                        }
                    }
                    break;
                case attack_direction.Down:
                    if (Result != ShipType.None)
                    {
                        next_attack_move_y++;
                        if (next_attack_move_y > (bound_y_max - 1))
                        {
                            if (number_of_attack_cycles > min_ship_length)
                            {
                                if (HitTest(Result, number_of_attack_cycles) == true)
                                {
                                    //Found a ship and sunk it
                                    SaveSunkenShip(Result, hit_pos_x, hit_pos_y, 'V');
                                    UpdateMasterCoordinates(Result, hit_pos_x, hit_pos_y, 'V');
                                }
                            }
                            ResetAttack();
                            break;
                        }
                        number_of_attack_cycles++;
                        //check if we sunk a skip? 
                        if (HitTest(Result, number_of_attack_cycles) == true)
                        {
                            SaveSunkenShip(Result, hit_pos_x, hit_pos_y, 'V');
                            UpdateMasterCoordinates(Result, hit_pos_x, hit_pos_y, 'V');
                            ResetAttack();
                        }

                    }
                    else
                    {//miss
                        ResetAttack();
                    }
                    break;
                default:
                    break;
            }
        }
        //****************************************************************************************************************************
        private void ResetAttack()
        {
            direction = attack_direction.None;
            hit_pos_x = hit_pos_y = next_attack_move_x = next_attack_move_y = 0;
            number_of_attack_cycles = 0;
            Mode = attack_surface_mode.hunt_mode;
        }
        //****************************************************************************************************************************
        private void SaveHit(char Result, int pos_x, int pos_y)
        {
            ShipPosition Ship = new ShipPosition(pos_x, pos_y);

            if (opponent_battle_group.ContainsKey(Result) == false)
            {
                opponent_battle_group.Add(Result, Ship);
            }
        }
        //****************************************************************************************************************************
        private void RemoveHit(char Result)
        {
            if (opponent_battle_group.ContainsKey(Result) == true)
            {
                opponent_battle_group.Remove(Result);
            }

        }
        //****************************************************************************************************************************
        private void SaveSunkenShip(char Result, int pos_x, int pos_y, char Orientation)
        {
            ShipPosition Ship = new ShipPosition(pos_x, pos_y, Orientation);

            if (opponent_sunken_battle_group.ContainsKey(Result) == false)
            {
                opponent_sunken_battle_group.Add(Result, Ship);
                RemoveHit(Result);
            }

        }
        //****************************************************************************************************************************
        private bool HitTest(char Result, int AttackCycles)
        {
            if (opponent_battle_group.ContainsKey(Result) == true)
            {
                if (AttackCycles == GetShipLength(Result))
                {
                    return (true);
                }

            }
            return (false);

        }
        //****************************************************************************************************************************
        private void UpdateMasterCoordinates(char Result, int pos_x, int pos_y, char Orientation)
        {
            int Length = GetShipLength(Result);

            if (Orientation == ShipRotation.Horizontal)
            {
                //test of edge position
                if ((pos_x + Length) > (bound_x_max - 1))
                {
                    //reset master position
                    master_position_x = 0;
                    master_position_y++;
                    if (master_position_y > (bound_y_max - 1))
                    {
                        //Reached the bottom edge reset  Y
                        master_position_y = 0;
                    }
                }
                else
                {
                    //looks like it can fit
                    master_position_x = pos_x + Length;
                }

            }
            else if (Orientation == ShipRotation.Vertical)
            {
                master_position_x = pos_x + 1;
                if (master_position_x > (bound_x_max - 1))
                {
                    //reset master position
                    master_position_x = 0;
                    master_position_y++;
                    if (master_position_y > (bound_y_max - 1))
                    {
                        //Reached the bottom edge reset  Y
                        master_position_y = 0;
                    }
                }
            }

        }
        //****************************************************************************************************************************
        private bool HaveSeenShip(char Result)
        {
            if (opponent_sunken_battle_group.ContainsKey(Result) == true)
            {
                return (true);
            }

            return (false);
        }
        //****************************************************************************************************************************
        private int GetShipLength(char Result)
        {
            switch (char.ToUpper(Result))
            {
                case ShipType.Carrier:
                    return 5;
                case ShipType.Battleship:
                    return 4;
                case ShipType.Destroyer:
                    return 3;
                case ShipType.Submarine:
                    return 3;
                case ShipType.PatrolBoat:
                    return 2;
            }
            return 0;
        }

        private char[,] AttackHistory;
        private static int master_position_x;
        private static int master_position_y;
        private static int hit_pos_x;
        private static int hit_pos_y;
        private static int next_attack_move_x;
        private static int next_attack_move_y;
        private attack_surface_mode Mode;
        private int number_of_attack_cycles;
        private attack_direction direction;
        private int bound_x_max;
        private int bound_y_max;
        private const int min_ship_length = 2;
        private Dictionary<char, ShipPosition> opponent_battle_group;
        private Dictionary<char, ShipPosition> opponent_sunken_battle_group;

    }
    //#*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*##*#*#*#
    public class Warfare : BattleshipAgent
    {
        //****************************************************************************************************************************
        public Warfare()
        {
            my_battle_group = new List<battle_ship>();

            opponent_attack_surface = new AttackSurface();
            opponent_attack_surface.Initialize();

            war_name = "captain marvel";
        }
        //****************************************************************************************************************************
        public override string GetNickname()
        {
            return (war_name);
        }
        //****************************************************************************************************************************
        public override void SetOpponent(string opponent)
        {
            opponent_name = opponent;
        }
        //****************************************************************************************************************************
        public override BattleshipFleet PositionFleet()
        {
            BattleshipFleet ThisFlet = new BattleshipFleet();
            if (AddBattleGroup(ref my_battle_group) == true)
            {
                ThisFlet = GetFlet(my_battle_group);
            }
            return (ThisFlet);
        }
        //****************************************************************************************************************************
        public override GridSquare LaunchAttack()
        {
            GridSquare Move = new GridSquare();

            opponent_attack_surface.GetNextAttachCoordinates(ref Move);

            return (Move);
        }
        //****************************************************************************************************************************
        public override void DamageReport(char report)
        {
            opponent_attack_surface.UpdateAttackSurface(report);
        }
        //****************************************************************************************************************************
        private ShipPosition GetShipPosition(List<battle_ship> BattleshipGroup, char Type)
        {
            ShipPosition ThisPosition = new ShipPosition();
            foreach (battle_ship Ship in BattleshipGroup)
            {
                if (Ship.classification == Type)
                {
                    ThisPosition = Ship.GetPosition;
                }
            }
            return (ThisPosition);
        }
        //****************************************************************************************************************************
        public BattleshipFleet GetFlet(List<battle_ship> BattleshipGroup)
        {
            BattleshipFleet ThisFlet = new BattleshipFleet();
            ThisFlet.Carrier = GetShipPosition(BattleshipGroup, ShipType.Carrier);
            ThisFlet.Battleship = GetShipPosition(BattleshipGroup, ShipType.Battleship);
            ThisFlet.Destroyer = GetShipPosition(BattleshipGroup, ShipType.Destroyer);
            ThisFlet.Submarine = GetShipPosition(BattleshipGroup, ShipType.Submarine);
            ThisFlet.PatrolBoat = GetShipPosition(BattleshipGroup, ShipType.PatrolBoat);
            return (ThisFlet);
        }
        //****************************************************************************************************************************
        public bool AddBattleGroup(ref List<battle_ship> BattleshipGroup)
        {
            Random GridX = new Random();
            Random GridY = new Random();
            char[] ThisBattleGroup = { 'C', 'B', 'D', 'S', 'P' };
            char[] OrientationPlacement = { 'H', 'V' };
            int OrientationIndex = 0;

            int pos_x = GridX.Next(1, 9);
            int pos_y = GridY.Next(1, 9);

            foreach (char Type in ThisBattleGroup)
            {
                if (InsertShip(ref BattleshipGroup, Type, OrientationPlacement[OrientationIndex], pos_x, pos_y) == false)
                {
                    //Log error;   
                }

                // ternary conditional ref https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/conditional-operator
                OrientationIndex = OrientationIndex == 0 ? 1 : 0;
                pos_x = GridX.Next(1, 9);
                pos_y = GridY.Next(1, 9);
            }
            return (true);
        }
        //****************************************************************************************************************************
        public bool FindLocation(ref List<battle_ship> BattleshipGroup, char Type, ref char Orientation, ref int pos_x, ref int pos_y)
        {
            Orientation = 'H';
            for (pos_y = (y_coord_boundary-1); pos_y > 0; pos_y--)
            {
                for (pos_x = (x_coord_boundary-1); pos_x > 0; pos_x--)
                {
                    if (IsInTheaterOfOperations(ref BattleshipGroup, Type, Orientation, pos_x, pos_y) == true)
                    {
                        if (IsPositionTaken(ref BattleshipGroup, Type, Orientation, pos_x, pos_y) == false)
                        {
                            return (true);
                        }
                    }
                }
            }
            Orientation = 'V';
            for (pos_x = (x_coord_boundary-1); pos_x > 0; pos_x--)
            {
                for (pos_y = (y_coord_boundary -1); pos_y > 0; pos_y--)
                {
                    if (IsInTheaterOfOperations(ref BattleshipGroup, Type, Orientation, pos_x, pos_y) == true)
                    {
                        if (IsPositionTaken(ref BattleshipGroup, Type, Orientation, pos_x, pos_y) == false)
                        {
                            return (true);
                        }
                    }
                }
            }
            return (false);
        }
        //****************************************************************************************************************************
        public bool InsertShip(ref List<battle_ship> BattleshipGroup, char Type, char Orientation, int pos_x, int pos_y)
        {
            bool Failed = true;
            if (IsInTheaterOfOperations(ref BattleshipGroup, Type, Orientation, pos_x, pos_y) == true)
            {
                if (IsPositionTaken(ref BattleshipGroup, Type, Orientation, pos_x, pos_y) == false)
                {
                    if (AddShip(ref BattleshipGroup, Type, Orientation, pos_x, pos_y) == true)
                    {
                        return (true);
                    }
                }
            }

            char LocalOrientation = Orientation;
            int LocalPositionX = 0;
            int LocalPositionY = 0;
            if (FindLocation(ref BattleshipGroup, Type, ref LocalOrientation, ref LocalPositionX, ref LocalPositionY) == true)
            {
                if (AddShip(ref BattleshipGroup, Type, LocalOrientation, LocalPositionX, LocalPositionY) == true)
                {
                    return (true);
                }
            }
            
            return (false);
        }
        //****************************************************************************************************************************
        public bool IsInTheaterOfOperations(ref List<battle_ship> BattleshipGroup, char Type, char Orientation, int pos_x, int pos_y)
        {
            int ShipDisplacement = GetShipLength(Type) - 1;
            int NewPositionX = pos_x + ShipDisplacement;
            int NewPositionY = pos_y + ShipDisplacement;

            if ((pos_x >= 0 && pos_x <= (x_coord_boundary - 1)) && (pos_y >= 0 && pos_y <= (y_coord_boundary - 1)))
            {
                if ((NewPositionX > (x_coord_boundary - 1)) || (NewPositionY > (y_coord_boundary - 1)))
                {
                    return (false);
                }
                return (true);
            }
            return (false);
        }
        //****************************************************************************************************************************
        public bool IsPositionTaken(ref List<battle_ship> BattleshipGroup, char Type, char Orientation, int pos_x, int pos_y)
        {
            battle_ship NewShip = new battle_ship(Type, pos_x, pos_y, Orientation);

            foreach (battle_ship Ship in BattleshipGroup)
            {
                if (Ship.position == NewShip.position)
                {
                    return (true);
                }

            }
            return (false);
        }
        //****************************************************************************************************************************
        public bool AddShip(ref List<battle_ship> BattleshipGroup, char Type, char Orientation, int pos_x, int pos_y)
        {
            battle_ship NewShip = new battle_ship(Type, pos_x, pos_y, Orientation);

            foreach (battle_ship Ship in BattleshipGroup)
            {
                if (Ship.position == NewShip.position)
                {
                    return (false);
                }
            }
            BattleshipGroup.Add(NewShip);
            return (true);
        }
        //****************************************************************************************************************************
        private int GetShipLength(char Result)
        {
            switch (char.ToUpper(Result))
            {
                case ShipType.Carrier:
                    return 5;
                case ShipType.Battleship:
                    return 4;
                case ShipType.Destroyer:
                    return 3;
                case ShipType.Submarine:
                    return 3;
                case ShipType.PatrolBoat:
                    return 2;
            }
            return 0;
        }

        private List<battle_ship> my_battle_group;
        private AttackSurface opponent_attack_surface;
        private string war_name;
        private string opponent_name;
        private const int x_coord_boundary = 10;
        private const int y_coord_boundary = 10;

    }//end warfare
}
