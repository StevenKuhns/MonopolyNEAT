using System;
using System.Collections.Generic;

namespace MONOPOLY
{
    public class Tiles
    {
        public List<Tile> GetTiles()
        {
            List<Tile> tileList = new List<Tile>();

            tileList.Add(new Tile
            {
                Id = 0,
                Name = "GO",
                Owner = -1,
                Type = TileType.NONE,
                Color = TileColor.NONE,
                Cost = 0,
                Mortgage = 0,
                House = 0,
                Penalities = null
            });

            tileList.Add(new Tile
            {
                Id = 1,
                Name = "MEDITERRANEAN AVENUE",
                Owner = -1,
                Type = TileType.PROPERTY,
                Color = TileColor.BROWN,
                Cost = 60,
                Mortgage = 30,
                House = 50,
                Penalities = new int[6] {2,10,30,90,160,250}
            });

            tileList.Add(new Tile
            {
                Id = 2,
                Name = "COMMUNITY CHEST",
                Owner = -1,
                Type = TileType.CHEST,
                Color = TileColor.NONE,
                Cost = 0,
                Mortgage = 0,
                House = 0,
                Penalities = null
            });

            tileList.Add(new Tile
            {
                Id = 3,
                Name = "BALTIC AVENUE",
                Owner = -1,
                Type = TileType.PROPERTY,
                Color = TileColor.BROWN,
                Cost = 60,
                Mortgage = 30,
                House = 50,
                Penalities = new int[6] {4,20,60,180,320,450}
            });

            tileList.Add(new Tile
            {
                Id = 4,
                Name = "INCOME TAX",
                Owner = -1,
                Type = TileType.TAX,
                Color = TileColor.NONE,
                Cost = 200,
                Mortgage = 0,
                House = 0,
                Penalities = null
            });

            tileList.Add(new Tile
            {
                Id = 5,
                Name = "READING RAILROAD",
                Owner = -1,
                Type = TileType.TRAIN,
                Color = TileColor.NONE,
                Cost = 200,
                Mortgage = 100,
                House = 0,
                Penalities = new int[4] {25,50,100,200}
            });

            tileList.Add(new Tile
            {
                Id = 6,
                Name = "ORIENTAL AVENUE",
                Owner = -1,
                Type = TileType.PROPERTY,
                Color = TileColor.LIGHT_BLUE,
                Cost = 100,
                Mortgage = 50,
                House = 50,
                Penalities = new int[6] {6,30,90,270,400,550}
            });

            tileList.Add(new Tile
            {
                Id = 7,
                Name = "CHANCE",
                Owner = -1,
                Type = TileType.CHANCE,
                Color = TileColor.NONE,
                Cost = 0,
                Mortgage = 0,
                House = 0,
                Penalities = null
            });

            tileList.Add(new Tile
            {
                Id = 8,
                Name = "VERMONT AVENUE",
                Owner = -1,
                Type = TileType.PROPERTY,
                Color = TileColor.LIGHT_BLUE,
                Cost = 100,
                Mortgage = 50,
                House = 50,
                Penalities = new int[6] {6,30,90,270,400,550}
            });

            tileList.Add(new Tile
            {
                Id = 9,
                Name = "CONNECTICUT AVENUE",
                Owner = -1,
                Type = TileType.PROPERTY,
                Color = TileColor.LIGHT_BLUE,
                Cost = 120,
                Mortgage = 60,
                House = 50,
                Penalities = new int[6] {8,40,100,300,450,600}
            });

            tileList.Add(new Tile
            {
                Id = 10,
                Name = "VISIT JAIL",
                Owner = -1,
                Type = TileType.NONE,
                Color = TileColor.NONE,
                Cost = 0,
                Mortgage = 0,
                House = 0,
                Penalities = null
            });

            tileList.Add(new Tile
            {
                Id = 11,
                Name = "ST. CHARLES PLACE",
                Owner = -1,
                Type = TileType.PROPERTY,
                Color = TileColor.PINK,
                Cost = 140,
                Mortgage = 70,
                House = 100,
                Penalities = new int[6] {10,50,150,450,625,750}
            });

            tileList.Add(new Tile
            {
                Id = 12,
                Name = "ELECTRIC COMPANY",
                Owner = -1,
                Type = TileType.UTILITY,
                Color = TileColor.NONE,
                Cost = 0,
                Mortgage = 0,
                House = 0,
                Penalities = new int[2] {4,10}
            });

            tileList.Add(new Tile
            {
                Id = 13,
                Name = "STATES AVENUE",
                Owner = -1,
                Type = TileType.PROPERTY,
                Color = TileColor.PINK,
                Cost = 140,
                Mortgage = 70,
                House = 100,
                Penalities = new int[6] {10,50,150,450,625,750}
            });

            tileList.Add(new Tile
            {
                Id = 14,
                Name = "VIRGINIA AVENUE",
                Owner = -1,
                Type = TileType.PROPERTY,
                Color = TileColor.PINK,
                Cost = 160,
                Mortgage = 80,
                House = 100,
                Penalities = new int[6] {12,60,180,500,700,900}
            });

            tileList.Add(new Tile
            {
                Id = 15,
                Name = "PENNSYLVANIA RAILROAD",
                Owner = -1,
                Type = TileType.TRAIN,
                Color = TileColor.NONE,
                Cost = 200,
                Mortgage = 100,
                House = 0,
                Penalities = new int[4] {25,50,100,200}
            });

            tileList.Add(new Tile
            {
                Id = 16,
                Name = "ST JAMES PLACE",
                Owner = -1,
                Type = TileType.PROPERTY,
                Color = TileColor.ORANGE,
                Cost = 180,
                Mortgage = 90,
                House = 100,
                Penalities = new int[6] {14,70,200,550,750,950}
            });

            tileList.Add(new Tile
            {
                Id = 17,
                Name = "COMMUNITY CHEST",
                Owner = -1,
                Type = TileType.CHEST,
                Color = TileColor.NONE,
                Cost = 0,
                Mortgage = 0,
                House = 0,
                Penalities = null
            });

            tileList.Add(new Tile
            {
                Id = 18,
                Name = "TENNESSEE AVENUE",
                Owner = -1,
                Type = TileType.PROPERTY,
                Color = TileColor.ORANGE,
                Cost = 180,
                Mortgage = 90,
                House = 100,
                Penalities = new int[6] {14,70,200,550,750,950}
            });

            tileList.Add(new Tile
            {
                Id = 19,
                Name = "NEW YORK AVENUE",
                Owner = -1,
                Type = TileType.PROPERTY,
                Color = TileColor.ORANGE,
                Cost = 200,
                Mortgage = 100,
                House = 100,
                Penalities = new int[6] {16,90,220,600,800,1000}
            });

            tileList.Add(new Tile
            {
                Id = 20,
                Name = "FREE PARKING",
                Owner = -1,
                Type = TileType.NONE,
                Color = TileColor.NONE,
                Cost = 0,
                Mortgage = 0,
                House = 0,
                Penalities = null
            });

            tileList.Add(new Tile
            {
                Id = 21,
                Name = "KENTUCKY AVENUE",
                Owner = -1,
                Type = TileType.PROPERTY,
                Color = TileColor.RED,
                Cost = 220,
                Mortgage = 110,
                House = 150,
                Penalities = new int[6] {18,90,250,700,875,1050}
            });

            tileList.Add(new Tile
            {
                Id = 22,
                Name = "CHANCE",
                Owner = -1,
                Type = TileType.CHANCE,
                Color = TileColor.NONE,
                Cost = 0,
                Mortgage = 0,
                House = 0,
                Penalities = null
            });

            tileList.Add(new Tile
            {
                Id = 23,
                Name = "INDIANA AVENUE",
                Owner = -1,
                Type = TileType.PROPERTY,
                Color = TileColor.RED,
                Cost = 220,
                Mortgage = 110,
                House = 150,
                Penalities = new int[6] {18,90,250,700,875,1050}
            });

            tileList.Add(new Tile
            {
                Id = 24,
                Name = "ILLINOIS AVENUE",
                Owner = -1,
                Type = TileType.PROPERTY,
                Color = TileColor.RED,
                Cost = 240,
                Mortgage = 120,
                House = 150,
                Penalities = new int[6] {20,100,300,750,925,1100}
            });

            tileList.Add(new Tile
            {
                Id = 25,
                Name = "B & O RAILROAD",
                Owner = -1,
                Type = TileType.TRAIN,
                Color = TileColor.NONE,
                Cost = 200,
                Mortgage = 100,
                House = 0,
                Penalities = new int[4] {25,50,100,200}
            });

            tileList.Add(new Tile
            {
                Id = 26,
                Name = "ATLANTIC AVENUE",
                Owner = -1,
                Type = TileType.PROPERTY,
                Color = TileColor.YELLOW,
                Cost = 260,
                Mortgage = 130,
                House = 150,
                Penalities = new int[6] {22,110,330,800,975,1150}
            });

            tileList.Add(new Tile
            {
                Id = 27,
                Name = "VENTNOR AVENUE",
                Owner = -1,
                Type = TileType.PROPERTY,
                Color = TileColor.YELLOW,
                Cost = 260,
                Mortgage = 130,
                House = 150,
                Penalities = new int[6] {22,110,330,800,975,1150}
            });

            tileList.Add(new Tile
            {
                Id = 28,
                Name = "WATER WORKS",
                Owner = -1,
                Type = TileType.UTILITY,
                Color = TileColor.NONE,
                Cost = 0,
                Mortgage = 0,
                House = 0,
                Penalities = new int[2] {4,10}
            });

            tileList.Add(new Tile
            {
                Id = 29,
                Name = "MARVIN GARDENS",
                Owner = -1,
                Type = TileType.PROPERTY,
                Color = TileColor.YELLOW,
                Cost = 280,
                Mortgage = 140,
                House = 150,
                Penalities = new int[6] {24,120,360,850,1025,1200}
            });

            tileList.Add(new Tile
            {
                Id = 30,
                Name = "GO TO JAIL",
                Owner = -1,
                Type = TileType.JAIL,
                Color = TileColor.NONE,
                Cost = 0,
                Mortgage = 0,
                House = 0,
                Penalities = null
            });

            tileList.Add(new Tile
            {
                Id = 31,
                Name = "PACIFIC AVENUE",
                Owner = -1,
                Type = TileType.PROPERTY,
                Color = TileColor.GREEN,
                Cost = 300,
                Mortgage = 150,
                House = 200,
                Penalities = new int[6] {26,130,390,900,1100,1275}
            });

            tileList.Add(new Tile
            {
                Id = 32,
                Name = "NORTH CAROLINA AVENUE",
                Owner = -1,
                Type = TileType.PROPERTY,
                Color = TileColor.GREEN,
                Cost = 300,
                Mortgage = 150,
                House = 200,
                Penalities = new int[6] {26,130,390,900,1100,1275}
            });

            tileList.Add(new Tile
            {
                Id = 33,
                Name = "COMMUNITY CHEST",
                Owner = -1,
                Type = TileType.CHEST,
                Color = TileColor.NONE,
                Cost = 0,
                Mortgage = 0,
                House = 0,
                Penalities = null
            });

            tileList.Add(new Tile
            {
                Id = 34,
                Name = "PENNSYLVANIA AVENUE",
                Owner = -1,
                Type = TileType.PROPERTY,
                Color = TileColor.GREEN,
                Cost = 320,
                Mortgage = 160,
                House = 200,
                Penalities = new int[6] {28,150,450,1000,1200,1400}
            });

            tileList.Add(new Tile
            {
                Id = 35,
                Name = "SHORT LINE RAILROAD",
                Owner = -1,
                Type = TileType.TRAIN,
                Color = TileColor.NONE,
                Cost = 200,
                Mortgage = 100,
                House = 0,
                Penalities = new int[4] {25,50,100,200}
            });

            tileList.Add(new Tile
            {
                Id = 36,
                Name = "CHANCE",
                Owner = -1,
                Type = TileType.CHANCE,
                Color = TileColor.NONE,
                Cost = 0,
                Mortgage = 0,
                House = 0,
                Penalities = null
            });

            tileList.Add(new Tile
            {
                Id = 37,
                Name = "PARK PLACE",
                Owner = -1,
                Type = TileType.PROPERTY,
                Color = TileColor.DARK_BLUE,
                Cost = 350,
                Mortgage = 175,
                House = 200,
                Penalities = new int[6] {35,175,500,1100,1300,1500}
            });

            tileList.Add(new Tile
            {
                Id = 38,
                Name = "LUXURY TAX",
                Owner = -1,
                Type = TileType.TAX,
                Color = TileColor.NONE,
                Cost = 100,
                Mortgage = 0,
                House = 0,
                Penalities = null
            });

            tileList.Add(new Tile
            {
                Id = 39,
                Name = "BOARDWALK",
                Owner = -1,
                Type = TileType.PROPERTY,
                Color = TileColor.DARK_BLUE,
                Cost = 400,
                Mortgage = 200,
                House = 200,
                Penalities = new int[6] {50,200,600,1400,1700,2000}
            });

            tileList.Add(new Tile
            {
                Id = 40,
                Name = "GO",
                Owner = -1,
                Type = TileType.NONE,
                Color = TileColor.NONE,
                Cost = 0,
                Mortgage = 0,
                House = 0,
                Penalities = null
            });

            return tileList;
        }

    }

    public class Tile
    {
        public int Id;
        public string Name;
        public int Owner;
        public TileType Type;
        public TileColor Color;
        public int Cost;
        public int Mortgage;
        public int House;
        public int[] Penalities;
    }

    public enum TileType
    {
        NONE,
        PROPERTY,
        TRAIN,
        UTILITY,
        CHANCE,
        CHEST,
        TAX,
        JAIL
    }

    public enum TileColor
    {
        NONE,
        BROWN,
        LIGHT_BLUE,
        PINK,
        ORANGE,
        RED,
        YELLOW,
        GREEN,
        DARK_BLUE
    }
}