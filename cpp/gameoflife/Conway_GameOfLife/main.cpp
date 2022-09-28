#include <iostream>

namespace Conway
{
	namespace GameOfLife 
	{
		// Used to represent neighbours status.
		enum class CellState
		{
			EMPTY = 0x0000,
			ALIVE = 0x0001
		};

		// World as a grid.
		struct World
		{
			// World size.
			static constexpr int SIZE_COLUMN = 86;
			static constexpr int SIZE_ROW = 86;

			// Custom input.
			// Initial colony.
			static CellState Initiate(int x, int y)
			{
				if (x == 4)
				{
					if (y == 1)
					{
						return CellState::ALIVE;
					}
					if (y == 2)
					{
						return CellState::ALIVE;
					}
					if (y == 3)
					{
						return CellState::ALIVE;
					}
				}
			}
		};

		struct Cell
		{
			// Position X.
			int x;

			// Position Y.
			int y;

			// Its own state.
			CellState state;

			// Check whether neighbour is alive.
			void CheckNeighbour(Cell* cell, int *aliveCounter) 
			{
				switch (cell->state)
				{
				case Conway::GameOfLife::CellState::ALIVE:
					// Dereference first.
					// Add after.
					*(aliveCounter) += 1;
					return;
				}
			}

			// How many alive neighbours are there.
			int CountAlive(Cell cells[][Conway::GameOfLife::World::SIZE_ROW + 2])
			{
				// temporary container.
				int aliveCounter = 0;

				// QWE - Upper row.
				// ASD - Ground row.
				// ZXC - Lower row.
				// S - is us, no need to computer.

				// Q - position. 
				CheckNeighbour(&cells[y - 1][x - 1], &aliveCounter);

				// W - position.
				CheckNeighbour(&cells[y - 1][x], &aliveCounter);

				// E - position.
				CheckNeighbour(&cells[y - 1][x + 1], &aliveCounter);

				// A - position.
				CheckNeighbour(&cells[y][x - 1], &aliveCounter);

				// S - is our position.

				// D - position.
				CheckNeighbour(&cells[y][x + 1], &aliveCounter);

				// Z - position.
				CheckNeighbour(&cells[y + 1][x - 1], &aliveCounter);

				// X - position.
				CheckNeighbour(&cells[y + 1][x], &aliveCounter);

				// C - position.
				CheckNeighbour(&cells[y + 1][x + 1], &aliveCounter);

				// Return to main flow.
				return aliveCounter;
			}

			// Evaluate function.
			CellState Evaluate(Cell cells[][Conway::GameOfLife::World::SIZE_ROW + 2])
			{
				int aliveCounter = CountAlive(cells);

				// Loneliness.
				if (aliveCounter <= 1) 
				{
					return (Conway::GameOfLife::CellState::EMPTY);
				}
				// Overcrowing.
				else if (aliveCounter >= 4) 
				{
					return (Conway::GameOfLife::CellState::EMPTY);
				}
				// Procreation.
				else 
				{
					return (Conway::GameOfLife::CellState::ALIVE);
				}
			}

			// Returns current representation of the cell 
			// based on the state of the cell.
			char Show() 
			{
				switch (state)
				{
				case Conway::GameOfLife::CellState::EMPTY:
					return ('O');
				case Conway::GameOfLife::CellState::ALIVE:
					return ('X');
				default:
					// This should never happen.
					return ('N');
				}
			}

			// Copy cells to the right ghost borders.
			// +2 because we include borders as well.
			static void SummonGhosts(Cell cells[][Conway::GameOfLife::World::SIZE_ROW + 2])
			{
				// Right Middle Borders. +1 because we exclude left and right borders.
				for (int rightBorderY = 1; rightBorderY < Conway::GameOfLife::World::SIZE_COLUMN + 1; rightBorderY++)
				{
					// +1 because we want to reach the very right border and indices start from 0.
					cells[rightBorderY][Conway::GameOfLife::World::SIZE_ROW + 1] = cells[rightBorderY][1];
				}

				// Left Middle Borders. +1 because we exclude left and right borders.
				for (int leftBorderY = 1; leftBorderY < Conway::GameOfLife::World::SIZE_COLUMN + 1; leftBorderY++)
				{
					// Counting started from 0, however, 0's position is taken by left ghost border.
					cells[leftBorderY][0] = cells[leftBorderY][Conway::GameOfLife::World::SIZE_ROW];
				}

				//// Top Middle Borders. +1 because we exclude top and bottom borders.
				for (int topBorderX = 1; topBorderX < Conway::GameOfLife::World::SIZE_ROW + 1; topBorderX++)
				{
					// Counting started from 0, however, 0's position is taken by top ghost border.
					cells[0][topBorderX] = cells[Conway::GameOfLife::World::SIZE_COLUMN][topBorderX];
				}


				// Bottom Middle Borders. +1 because we exclude top and bottom borders.
				for (int bottomBorderX = 1; bottomBorderX < Conway::GameOfLife::World::SIZE_ROW + 1; bottomBorderX++)
				{
					// +1 because we want to reach the very bottom border and indices start from 0.
					cells[Conway::GameOfLife::World::SIZE_COLUMN + 1][bottomBorderX] = cells[1][bottomBorderX];
				}

				// Top left corner. Copy from bottom right edge of the world cells to top left corner ghost cell.
				cells[0][0] = cells[Conway::GameOfLife::World::SIZE_COLUMN][Conway::GameOfLife::World::SIZE_ROW];

				// Top right corner. +1 because top right ghost corner is beyond world edge.
				cells[0][Conway::GameOfLife::World::SIZE_ROW + 1] = cells[Conway::GameOfLife::World::SIZE_COLUMN][1];

				// Bottom right corner. +1 because bottom right ghost corner is beyond world edge.
				cells[Conway::GameOfLife::World::SIZE_COLUMN + 1][Conway::GameOfLife::World::SIZE_ROW + 1] = cells[1][1];

				// Bottom left corner. +1 because bottom left ghost corner is beyond world edge.
				cells[Conway::GameOfLife::World::SIZE_COLUMN + 1][0] = cells[1][Conway::GameOfLife::World::SIZE_ROW];
			}
		};
	}
}

int main(int argc, char** argv)
{
	// Column - Y; Row - X. Origin left top corner.
	Conway::GameOfLife::Cell cells[Conway::GameOfLife::World::SIZE_COLUMN + 2][Conway::GameOfLife::World::SIZE_ROW + 2];

	// Game Field population. Offset by one.
	for (int column = 1; column < Conway::GameOfLife::World::SIZE_COLUMN + 1 ; column++)
	{
		for (int row = 1; row < Conway::GameOfLife::World::SIZE_ROW + 1; row++)
		{
			// store indices as positions of the cell.
			cells[column][row].y = column;
			cells[column][row].x = row;

			// Retrieve user input.
			cells[column][row].state = Conway::GameOfLife::World::Initiate(row, column);
		}
	}

	// Create ghost cells and popualte them.
	Conway::GameOfLife::Cell::SummonGhosts(cells);

	// Output welcome message.
	std::cout << "Entire world map.\n\n";

	// Output positions. +2 to include ghost borders.
	for (int i = 0; i < Conway::GameOfLife::World::SIZE_COLUMN + 2; i++)
	{
		for (int j = 0; j < Conway::GameOfLife::World::SIZE_ROW + 2; j++)
		{
			std::cout << '(' << cells[i][j].x << ',' << cells[i][j].y << ") ";
		}
		std::cout << '\n';
	}

	// World label.
	std::cout << "\nInitial state of the world with ghost borders.\n\n";

	// Output start state of the desk. +2 to include ghost borders.
	for (int i = 0; i < Conway::GameOfLife::World::SIZE_COLUMN + 2; i++)
	{
		for (int j = 0; j < Conway::GameOfLife::World::SIZE_ROW + 2; j++)
		{
			std::cout << cells[i][j].Show() << ' ';
		}
		// Separating rows. (X-axis).
		std::cout << '\n';
	}

	// Section separator.
	std::cout << "\nPress Enter to iterate or B to break out of the loop.\n";

	// Output controllers.
	char input = getchar();

	// Temporary storage container for the cells states.
	Conway::GameOfLife::Cell cellsBuffer[Conway::GameOfLife::World::SIZE_COLUMN + 2][Conway::GameOfLife::World::SIZE_COLUMN + 2];

	// Loop output.
	while (input != 'b')
	{
		// Calculate all states without affecting them.
		// +1 because we update only world map, not ghost edges.
		for (int i = 1; i < Conway::GameOfLife::World::SIZE_COLUMN + 1; i++)
		{
			for (int j = 1; j < Conway::GameOfLife::World::SIZE_ROW + 1; j++)
			{
				cellsBuffer[i][j].state = cells[i][j].Evaluate(cells);
			}
		}

		// Write all states back to original pool of cells.
		// +1 because we update only world map, not ghost edges.
		for (int i = 1; i < Conway::GameOfLife::World::SIZE_COLUMN + 1; i++)
		{
			for (int j = 1; j < Conway::GameOfLife::World::SIZE_ROW + 1; j++)
			{
				cells[i][j].state = cellsBuffer[i][j].state;
			}
		}

		// Update ghost cells and populate them. 
		// Must be done after states been updated.
		Conway::GameOfLife::Cell::SummonGhosts(cells);

		// Represent result to the console.
		// +1 because we update only world map, not ghost edges.
		for (int i = 1; i < Conway::GameOfLife::World::SIZE_COLUMN + 1; i++)
		{
			for (int j = 1; j < Conway::GameOfLife::World::SIZE_ROW + 1; j++)
			{
				std::cout << cells[i][j].Show() << ' ';
			}
			// Separating rows. (X-axis).
			std::cout << '\n';
		}

		// Section separator.
		std::cout << '\n';

		// Expect new input.
		input = getchar();
	}

	// Finalise streaming.
	std::cout << std::endl;

	return(0);
}