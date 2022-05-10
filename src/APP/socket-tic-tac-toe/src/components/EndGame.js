import React from 'react';

function EndGame({ restartGame, player, draw, winCount, clearHistory }) {
    return (
        <div className="end-game-screen">
            {!draw && <span className="win-text">{player ? 'O won' : 'X won'}</span>}
            {draw && <span className="win-text">DRAW GAME</span>}

            <span className="win-history">
                X's wins: {winCount.X} <br /> O's wins: {winCount.O}{' '}
            </span>

            <button className="btn" onClick={restartGame}>
                RESTART GAME
            </button>
            <button className="btn" onClick={clearHistory}>
                CLEAR HISTORY
            </button>
        </div>
    );
}
export default EndGame;
