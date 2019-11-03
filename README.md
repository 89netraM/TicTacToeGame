# TicTacToe

Play TicTacToe against other people or against other programs. Or why not have
two programs face off against each other?

## Download

Head over to the ["Releases" section](https://github.com/89netraM/TicTacToeGame/releases) to download the latest compiled version for either Windows or Linux.

## Usage

### Arguments
| Argument              | Effect                                               |
|-----------------------|------------------------------------------------------|
| `--help`              | Prints out the help.                                 |
| `--size <int>`        | Sets the size of the game board.                     |
| `--goal-length <int>` | Sets the length required for victory. Default is same as the board size. |
| `--cross <string>`    | Path to an executable that should act as the cross player. |
| `--circle <string>`   | Path to an executable that should act as the circle player. |

### Running

When the program is up and running it will print out a view of the board with
"X", "O" for markers, and " " (space) for empty squares. The player (or player
program) should then write what row and column they want to place their marker
on. Then the other player gets a print out of what the updated board looks like
and they have to enter where they want to place their marker. This goes on
until either the board is full or one player has won.

### Program as player

When a program is used as a player the program receives three starting arguments:
1. An integer describing the size of the board.
2. An integer naming the length required to win.
3. `X` or `O` telling the program which marker it's going to play as.

### Example

A person versus person game:

```shell
>MoreTec.Player.exe --size 3
<   
<   
<   
>1 1
<   
< X 
<   
>0 1
< O 
< X 
<   
>0 2
< OX
< X 
<   
>1 0
< OX
<OX 
<   
>2 0
<Game Over: Cross Won!
<Game Over: Cross Won!
```