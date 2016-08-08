using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Nebulous
{
	public class MapRenderer
	{
		//Because we'll load several tilesets at once, they're stored in an array of textures; we can predict the size
		//because they'll always be the same number of sets.
		private static Texture2D[] tilesets;
		private Rectangle[,,]	mapSurface;
		private Rectangle[] tilemap;
		private static MapObject mapdata;

		//This is the base values for our tiles. Manipulate these at runtime to get the different tile object sizes for rendering
		private const int TILE_WIDTH = 48;
		private const int TILE_HEIGHT = 24;

		public static int cameraX;
		public static int cameraY;

		private const int TILESETS_NUM = 3; //Default number

		public MapRenderer(string mapfile)
		{
			//Read and sort mapfile data into memory objects here
			mapdata =  new MapObject(mapfile);
			//Initialize the tilesets array
			tilesets = new Texture2D[TILESETS_NUM];
			mapSurface = new Rectangle[mapdata.mapWidth,mapdata.mapHeight,3];
			CreateMapSurface();
			CreateTileMap();
			//Test invocation after initialization

		}

		private void CreateMapSurface()
		{
			int offsetX = 220;
			int offsetY = 200;
			int indexX = 0;
			int indexY = 0;
			int isoX = 0;
			int isoY = 0;
			foreach(int path in mapdata.layerFloor)
			{
				while (indexX < mapdata.mapWidth)
				{
					indexY = 0;
					while (indexY < mapdata.mapHeight)
					{
						//Refactored to separate the iso coords from the actual function
						isoX = ((indexX - indexY) * ((TILE_WIDTH / 2)));
						isoY = ((indexX + indexY) * ((TILE_HEIGHT / 2)));
						//Project the surface
						mapSurface[indexX,indexY,0] = new Rectangle(offsetX + isoX, offsetY + isoY, TILE_WIDTH, TILE_HEIGHT);
						indexY++;
					}
					indexX++;
				}	
			}
			indexX = 0;
			indexY = 0;
		}

		private void CreateTileMap()
		{

		}

		public void LoadContent(ContentManager contentManager)
		{
			for (int i = 0; i < tilesets.Length; i++)
			{
				tilesets[i] = contentManager.Load<Texture2D>("Graphics/Environments/" + mapdata.tilesets[i]);
			}
		}

		public void Update()
		{

		}

		public void Draw(SpriteBatch spriteBatch)
		{
			//A test to make sure the maprenderer's render target is the correct positioning
			Rectangle sourceRect = new Rectangle(0, 0, TILE_WIDTH, TILE_HEIGHT);

			for (int y = 0; y < mapdata.mapHeight; y++)
			{
				for (int x = 0; x < mapdata.mapWidth; x++)
				{
					spriteBatch.Draw(tilesets[0], mapSurface[x, y, 0], sourceRect, Color.White);
				}
			}
		}
	}

	#region MapObject
	//This creates a dynamic object from our XML datastream
	public class MapObject
	{
		public int[] layerFloor;
		public int[] layerWalls;
		public int[] layerObjects;

		public int mapWidth;
		public int mapHeight;

		public string[] tilesets;

		private StreamReader mapfile;
		private string mapdata;

		public MapObject(string filename)
		{
			mapfile = File.OpenText(filename);
			mapdata = mapfile.ReadToEnd();
			char[] delimiters = {',', '=', '\n', ' '};

			GetDimensions();
			layerFloor = new int[mapWidth * mapHeight];
			layerWalls = new int[mapWidth * mapHeight];
			layerObjects = new int[mapWidth * mapHeight];

			GetLayerData(delimiters);
			GetTilesetFiles(delimiters);

		}

		//Parse the text stream into tile layer data
		private void GetLayerData(char[] delimiters)
		{
			//Load the layer data into a string and process the data from it
			int index_first = mapdata.IndexOf("[layer]") + "[layer]".Length;
			int index_last = mapdata.Length;
			string rawLayerData = mapdata.Substring(index_first, index_last - index_first);

			//Replace all the extra crap with empty space
			rawLayerData = rawLayerData.Replace("[layer]","");
			rawLayerData = rawLayerData.Replace("type=", "");
			rawLayerData = rawLayerData.Replace("data=", "");
			rawLayerData = rawLayerData.Replace("floor", "");
			rawLayerData = rawLayerData.Replace("objects", "");
			rawLayerData = rawLayerData.Replace("walls", "");
			rawLayerData = rawLayerData.Replace("\r\n", "");
			rawLayerData = rawLayerData.Trim();

			string[] layer_data_processed = rawLayerData.Split(delimiters);
			int index = 0;
			int path_index = 0;
			foreach (string path in layer_data_processed)
			{
				if (path_index == mapWidth * mapHeight - 1)
				{
					path_index = 0;
				}
				if (index < (mapWidth * mapHeight))
				{
					layerFloor[path_index] = Int32.Parse(path);
				}
				if (index >= layerFloor.Length && index < (layerFloor.Length * 2))
				{
				    layerWalls[path_index]	= Int32.Parse(path);
				}
				if (index >= (layerFloor.Length * 2))
				{
					layerObjects[path_index] = Int32.Parse(path);
				}
				path_index++;
				index++;
			}
			
		}

		//Get the tileset filenames
		private void GetTilesetFiles(char[] delimiters)
		{
			//Prepare the string to be cut up into pieces
			int index_first = mapdata.IndexOf("[tilesets]") + "[tilesets]".Length;
			int index_last = mapdata.IndexOf("[layer]");
			string rawTilesetData = mapdata.Substring(index_first, index_last - index_first);

			//Remove the extra crap
			rawTilesetData = rawTilesetData.Replace("tileset=", "");
			rawTilesetData = rawTilesetData.Trim();
			tilesets = rawTilesetData.Split('\n');

			//Remove the last pieces of extraneous information and put the filenames
			//into the tileset file path array
			int i = 0;
			foreach (string path in tilesets){
				index_last = path.IndexOf(',');
				tilesets[i] = path.Substring(0,index_last);
				i++;
			}

		}

		//Get the width and height of the map in tiles from the mapdata file
		private void GetDimensions()
		{
			//=============================================================================
			int index_first = mapdata.IndexOf("width=") + "width=".Length;
			int index_last = mapdata.IndexOf("height=");
			string rawWidth = mapdata.Substring(index_first, index_last - index_first);

			rawWidth = rawWidth.Trim();
			mapWidth = Int32.Parse(rawWidth);

			//=============================================================================
			index_first = mapdata.IndexOf("height=") + "height=".Length;
			index_last = mapdata.IndexOf("tilewidth=");
			string rawHeight = mapdata.Substring(index_first, index_last - index_first);

			rawHeight = rawHeight.Trim();
			mapHeight = Int32.Parse(rawHeight);
		}
	}
	#endregion
}