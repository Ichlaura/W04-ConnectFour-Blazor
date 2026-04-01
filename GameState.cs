namespace W04ConnectFour;

public class GameState
{
    private readonly byte[,] board = new byte[6, 7];

    public int CurrentTurn { get; private set; }
    public byte PlayerTurn => (byte)((CurrentTurn % 2) + 1);

    public enum WinState
    {
        None,
        Player1_Wins,
        Player2_Wins,
        Tie
    }

    public void ResetBoard()
    {
        for (int row = 0; row < 6; row++)
        {
            for (int col = 0; col < 7; col++)
            {
                board[row, col] = 0;
            }
        }

        CurrentTurn = 0;
    }

    public byte PlayPiece(byte col)
    {
        if (col > 6)
            throw new ArgumentException("Invalid column.");

        for (byte row = 6; row >= 1; row--)
        {
            if (board[row - 1, col] == 0)
            {
                board[row - 1, col] = PlayerTurn;
                CurrentTurn++;
                return row;
            }
        }

        throw new ArgumentException("That column is full.");
    }

    public WinState CheckForWin()
    {
        for (int row = 0; row < 6; row++)
        {
            for (int col = 0; col < 7; col++)
            {
                byte player = board[row, col];
                if (player == 0)
                    continue;

                if (col <= 3 &&
                    board[row, col + 1] == player &&
                    board[row, col + 2] == player &&
                    board[row, col + 3] == player)
                    return player == 1 ? WinState.Player1_Wins : WinState.Player2_Wins;

                if (row <= 2 &&
                    board[row + 1, col] == player &&
                    board[row + 2, col] == player &&
                    board[row + 3, col] == player)
                    return player == 1 ? WinState.Player1_Wins : WinState.Player2_Wins;

                if (row <= 2 && col <= 3 &&
                    board[row + 1, col + 1] == player &&
                    board[row + 2, col + 2] == player &&
                    board[row + 3, col + 3] == player)
                    return player == 1 ? WinState.Player1_Wins : WinState.Player2_Wins;

                if (row >= 3 && col <= 3 &&
                    board[row - 1, col + 1] == player &&
                    board[row - 2, col + 2] == player &&
                    board[row - 3, col + 3] == player)
                    return player == 1 ? WinState.Player1_Wins : WinState.Player2_Wins;
            }
        }

        if (CurrentTurn >= 42)
            return WinState.Tie;

        return WinState.None;
    }
}