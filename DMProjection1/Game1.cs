using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

//using 

namespace DMProjection1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        DMControls dm = new DMControls();
        Palatte palatte = new Palatte();

        Selected selected = new Selected();

        List<Entity> allEntities = new List<Entity>();

        GameControl game;
        //= new GameControl(new Vector2(0,0),
        //new Vector2(0,0),
        //new Vector2(graphicsma))

        MouseTracker mouseTracker = new MouseTracker();
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //DMControls dMControls
            this.IsMouseVisible = true;
            base.Initialize();
            dm.Show();
            dm.UpdateEvent += UpdateReceived;

            palatte.SelectUpdateEvent += PalatteUpdate;
            palatte.Show();
            
            game = new GameControl(new Vector2(0, 0),
                new Vector2(0, 0),
                new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));
            game.background = new Texture2D(graphics.GraphicsDevice, 1, 1);
            game.background.SetData(new Color[] { Color.CornflowerBlue });
            //Player display area
            ListBox pList = new ListBox(game.absoluteLocation,
                new Vector2(0, 0),
                new Vector2(150, graphics.PreferredBackBufferHeight / 2),
                graphics.GraphicsDevice);
            pList.selectEvent += selected.selectEventReceiverFromList;
            game.Add(pList);
            //game.Add(new ListBox(game.absoluteLocation,
            //    new Vector2(0, 0),
            //    new Vector2(75, graphics.PreferredBackBufferHeight / 2)));
            //Ally / neutral display area
            ListBox aList = new ListBox(game.absoluteLocation,
                new Vector2(0, graphics.PreferredBackBufferHeight / 2),
                new Vector2(150, graphics.PreferredBackBufferHeight / 2),
                graphics.GraphicsDevice);
            aList.selectEvent += selected.selectEventReceiverFromList;
            game.Add(aList);
            //game.Add(new ListBox(game.absoluteLocation,
            //    new Vector2(0, graphics.PreferredBackBufferHeight/2),
            //    new Vector2(75, graphics.PreferredBackBufferHeight / 2)));
            //Enemy display area
            ListBox eList = new ListBox(
                game.absoluteLocation,
                new Vector2(graphics.PreferredBackBufferWidth - 150, 0),
                new Vector2(150, graphics.PreferredBackBufferHeight),
                graphics.GraphicsDevice
                );
            eList.selectEvent += selected.selectEventReceiverFromList;
            game.Add(eList);
            //game.Add(new ListBox(
            //    game.absoluteLocation,
            //    new Vector2(graphics.PreferredBackBufferWidth - 75, 0),
            //    new Vector2(75, graphics.PreferredBackBufferHeight)
            //    ));
            Grid g = new Grid(game.absoluteLocation,
                new Vector2(150, 0),
                new Vector2(graphics.PreferredBackBufferWidth - 300, graphics.PreferredBackBufferHeight),
                20, 20,
                graphics.GraphicsDevice);
            g.background = new Texture2D(graphics.GraphicsDevice, 1, 1);
            g.background.SetData(new Color[] { Color.CornflowerBlue });
            g.selectEvent += selected.selectEventReceiverFromTile;
            game.Add(g);

            mouseTracker.MouseUpEvent += MouseClickToGame;
            //for(int i = 0; i < 400; i++)
            //{
            //    (Tile)(g.children[i])
            //}
            //for (int i = 0; i < )
            //game.Add(new Grid(
            //    game.absoluteLocation,
            //    new Vector2(75, 0),
            //    new Vector2(graphics.PreferredBackBufferWidth - 150, graphics.PreferredBackBufferHeight),
            //    20, 20));
            //for(int i = 0; i < game[3])
        }

        public void MouseClickToGame(object sender)
        {
            MouseTracker m = (MouseTracker)sender;
            game.ClickCheck(new Rectangle(Convert.ToInt32(m.upPoint.X), Convert.ToInt32(m.upPoint.Y), 1, 1));
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            var mState = Mouse.GetState();
            mouseTracker.Mouse(mState, gameTime);


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            //Draw display areas
            game.Draw(spriteBatch);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
            spriteBatch.End();
        }

        public void UpdateReceived()
        {
            EntityList(dm.roster, dm.enemies, dm.allies);
        }

        public void EntityList(PC[] pcs, List<NPC> enemies, List<NPC> allies)
        {
            allEntities.Clear();
            allEntities.Add(pcs[0]); allEntities.Add(pcs[1]); allEntities.Add(pcs[2]);
            for(int i = 0; i < enemies.Count; i++)
            {
                allEntities.Add(enemies[i]);
            }
            for(int i = 0; i < allies.Count; i++)
            {
                allEntities.Add(allies[i]);
            }
        }

        public void PalatteUpdate(System.Drawing.Image im)
        {
            //GraphicsDevice.get
            selected.selectReceiverFromPalatte(graphics.GraphicsDevice, im);
        }
    }






    #region Controls
    public class GameControl
    {
        public List<GameControl> children = new List<GameControl>();
        public Vector2 absoluteLocation;
        public Vector2 location;
        public Vector2 widthHeight;

        public Texture2D background;

        private Rectangle rect;

        public GameControl() { }

        public GameControl(Vector2 parentLocation, Vector2 thisRelativeLocation, Vector2 thisDimensions)
        {
            absoluteLocation = parentLocation + thisRelativeLocation;
            location = thisRelativeLocation;
            widthHeight = thisDimensions;
            EvaluateRect();
        }

        public void Add(GameControl gc)
        {
            children.Add(gc);
        }

        public void EvaluateRect()
        {
            rect = new Rectangle(
                Convert.ToInt16(absoluteLocation.X),
                Convert.ToInt16(absoluteLocation.Y),
                Convert.ToInt16(widthHeight.X),
                Convert.ToInt16(widthHeight.Y));
        }

        public bool ClickCheck(Rectangle clickLoc)
        {
            if (clickLoc.Intersects(rect))
            {
                if(children.Count > 0)
                {
                    for(int i = 0; i < children.Count; i++)
                    {
                        if (children[i].ClickCheck(clickLoc))
                        {
                            return true;
                        }
                    }
                    return true;
                }
                else
                {
                    ClickActions();
                    return true;
                }
            }
            else { return false; }
        }

        public virtual void ClickActions()
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, rect, Color.White);
            for(int i = 0; i < children.Count; i++)
            {
                children[i].Draw(spriteBatch);
            }
        }
        
    }

    public class Grid : GameControl
    {
        public delegate void Selected(int type, int entity, Tile tile);
        public event Selected selectEvent;

        public Grid(Vector2 parentLocation, Vector2 thisRelativeLocation, Vector2 thisDimensions, 
            int cols, int rows, GraphicsDevice gd)
        {
            absoluteLocation = parentLocation + thisRelativeLocation;
            location = thisRelativeLocation;
            widthHeight = thisDimensions;
            EvaluateRect();

            for (int i = 0; i < cols; i++)
            {
                for(int j = 0; j < rows; j++)
                {
                    Tile gc = new Tile(
                        absoluteLocation, 
                        new Vector2(i * widthHeight.X / cols, j * widthHeight.Y / rows), 
                        new Vector2(widthHeight.X / cols, widthHeight.Y / rows),
                        gd
                        );
                    children.Add(gc);
                    gc.selectEvent += SelectEventReceiver;
                }
            }
        }

        public void SelectEventReceiver(int type, int entity, Tile tile)
        {
            selectEvent(type, entity, tile);
        }
    }

    public class Tile : GameControl
    {
        public int type;
        //public ref GameControl ReferencedGC;
        public int referencedEntityInd;

        public delegate void Selected(int type, int entity, Tile tile);
        public event Selected selectEvent;

        public Tile(Vector2 parentLocation, Vector2 thisRelativeLocation, Vector2 thisDimensions, GraphicsDevice gd)
        {
            absoluteLocation = parentLocation + thisRelativeLocation;
            location = thisRelativeLocation;
            widthHeight = thisDimensions;
            EvaluateRect();
            type = -1;
            background = new Texture2D(gd, 1, 1);
            background.SetData(new Color[] { Color.Blue });
        }

        public override void ClickActions()
        {
            base.ClickActions();
            selectEvent(type, referencedEntityInd, this);
        }

    }

    public class ListBox : GameControl
    {
        public delegate void Selected(int entity);
        public event Selected selectEvent;

        public ListBox(Vector2 parentLocation, Vector2 thisRelativeLocation, Vector2 thisDimensions, GraphicsDevice gd)
        {
            absoluteLocation = parentLocation + thisRelativeLocation;
            location = thisRelativeLocation;
            widthHeight = thisDimensions;
            EvaluateRect();
            background = new Texture2D(gd, 1, 1);
            background.SetData(new Color[] { Color.Black });
        }

        public void transferSelect(int entity)
        {
            selectEvent(entity);
        }

        public override void ClickActions()
        {
            base.ClickActions();

            //selectEvent()
        }
    }

    public class PCDisplay : GameControl
    {
        PC pc;

        public PCDisplay(Vector2 parentLocation, Vector2 thisRelativeLocation, Vector2 thisDimensions, GraphicsDevice gd)
        {
            absoluteLocation = parentLocation + thisRelativeLocation;
            location = thisRelativeLocation;
            widthHeight = thisDimensions;
            EvaluateRect();
            background = new Texture2D(gd, 1, 1);
            background.SetData(new Color[] { Color.Black });
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
            base.Draw(spriteBatch);
            if(pc != null)
            {
                spriteBatch.DrawString(_globals.defaultFont, pc.name, absoluteLocation, Color.White);
            }
        }
    }
    #endregion

    public class Selected
    {
        public Entity selectedEntity;
        public Tile selectedTile;

        public Texture2D paintbrush;
        public int paintType;

        public List<Entity> entities = new List<Entity>();

        public Selected()
        {

        }


        public void selectEventReceiverFromTile(int type, int entity, Tile tile)
        {
            if(type == -1)
            {
                if(paintbrush != null)
                {
                    tile.background = paintbrush;
                    tile.type = 0;
                }
                return;
            }
            else if(type == 0)//regular floor.  Move mechanics here.
            {
                selectedTile = tile;
            }
            else if(type == 1)//character.  Change selection.
            {
                selectedEntity = entities[entity];
            }
        }

        public void selectEventReceiverFromList(int entity)
        {
            selectedEntity = entities[entity];
        }

        public void selectReceiverFromPalatte(GraphicsDevice gd, System.Drawing.Image im)
        {

            //GraphicsDevice gd = new GraphicsDevice();
            using (System.IO.MemoryStream s = new System.IO.MemoryStream())
            {
                im.Save(s, System.Drawing.Imaging.ImageFormat.Png);
                s.Seek(0, System.IO.SeekOrigin.Begin);
                paintbrush = Texture2D.FromStream(gd, s);
            }


        }

        //public void GetTextureFromImage(GraphicsDevice gd, System.Drawing.Image im)
        //{

        //}
    }

    public class MouseTracker
    {
        public bool lastState = false;
        public Vector2 downPoint;
        public Vector2 upPoint;
        public double time;
        public bool mouseHold = false;

        public delegate void MouseUp(object sender);
        public event MouseUp MouseUpEvent;

        public MouseTracker()
        {

        }

        public void Mouse(MouseState mState, GameTime gameTime)
        {
            if (!lastState)
            {
                if(mState.LeftButton == ButtonState.Pressed)
                {
                    lastState = true;
                    downPoint = new Vector2(mState.X, mState.Y);
                    time = 0;
                    return;
                }
                else { return; }
            }
            else
            {
                if(mState.LeftButton == ButtonState.Released)
                {
                    lastState = false;
                    upPoint = new Vector2(mState.X, mState.Y);
                    MouseUpEvent(this);
                    return;
                }
                else
                {
                    time += gameTime.ElapsedGameTime.TotalSeconds;
                    if(time > 1)
                    {
                        mouseHold = true;
                    }
                    else { mouseHold = false; }
                    return;
                }
            }
        }
    }
}
