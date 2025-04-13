﻿using System.ComponentModel;

namespace ConcurrentProgramming.Model
{
    public class Ball : IBall, INotifyPropertyChanged
    {
        private int positionX;
        private int positionY;
        private int diameter = 20;
        private VectorTo velocity;

        public Ball(int positionX, int positionY)
        {
            this.positionX = positionX;
            this.positionY = positionY;
            velocity = new VectorTo(1, 1);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int PositionX
        {
            get => positionX;
            set
            {
                if (positionX != value)
                {
                    positionX = value;
                    OnPropertyChanged(nameof(PositionX));
                }
            }
        }

        public int PositionY
        {
            get => positionY;
            set
            {
                if (positionY != value)
                {
                    positionY = value;
                    OnPropertyChanged(nameof(PositionY));
                }
            }
        }
        public int Diameter
        {
            get => diameter;
            set
            {
                if (diameter != value)
                {
                    diameter = value;
                    OnPropertyChanged(nameof(Diameter));
                }
            }
        }
        public VectorTo Velocity
        {
            get => velocity;
            set => velocity = value;
        }
    }
}
