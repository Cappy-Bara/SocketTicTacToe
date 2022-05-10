import React, { useState } from 'react';

import Square from './Square';
import EndGame from './EndGame';

const INITIAL = '';
const X_player = 'X';
const O_player = 'O';
const winCombination = [
    [0, 1, 2],
    [3, 4, 5],
    [6, 7, 8],
    [0, 3, 6],
    [1, 4, 7],
    [2, 5, 8],
    [0, 4, 8],
    [2, 4, 6],
];

function TicTacToe() {
    const [grid, setGrid] = useState(Array(9).fill(INITIAL));
    const [player, setPlayer] = useState(false);
    const [gameFinished, setGameFinished] = useState(false);
    const [draw, setDraw] = useState(false);
    const [winCount, setWinCount] = useState({ X: 0, O: 0 });

    function isGameOver() {
        if (!gameFinished) {
            //* X win check
            for (let i = 0; i < 8; i++) {
                if (
                    grid[winCombination[i][0]] === X_player && //check if any of win combination have just "X" or "O"
                    grid[winCombination[i][1]] === X_player &&
                    grid[winCombination[i][2]] === X_player
                ) {
                    setGameFinished(true);
                    setWinCount({ ...winCount, X: winCount.X + 1 });
                    console.log('X WON');
                    return;
                }
            }

            //* O win check
            for (let i = 0; i < 8; i++) {
                if (
                    grid[winCombination[i][0]] === O_player && //check if any of win combination have just "X" or "O"
                    grid[winCombination[i][1]] === O_player &&
                    grid[winCombination[i][2]] === O_player
                ) {
                    setGameFinished(true);
                    setWinCount({ ...winCount, O: winCount.O + 1 });
                    console.log('O WON');
                    return;
                }
            }
            //* Draw game check
            //No initial in squares, mean draw
            if (!grid.includes(INITIAL)) {
                setDraw(true);
                setGameFinished(true);
                console.log('DRAW');
            }
        }
    }

    function restartGame() {
        setGrid(Array(9).fill(INITIAL));
        setGameFinished(false);
        setDraw(false);
    }

    function clearHistory() {
        setWinCount({ X: 0, O: 0 });
        restartGame();
        console.log('clear history');
    }

    isGameOver();

    function handleClick(id) {
        setGrid(
            grid.map((item, index) => {
                if (index === id) {
                    if (player) {
                        return X_player;
                    } else {
                        return O_player;
                    }
                } else {
                    return item;
                }
            })
        );
        setPlayer(!player);
    }

    return (
        <div>
            <span className="win-history">
                X's wins: {winCount.X} <br /> O's wins: {winCount.O}{' '}
            </span>
            {gameFinished && (
                <EndGame
                    clearHistory={clearHistory}
                    winCount={winCount}
                    restartGame={restartGame}
                    player={player}
                    draw={draw}
                />
            )}
            <Square clickedArray={grid} handleClick={handleClick} />
        </div>
    );
}

export default TicTacToe;
