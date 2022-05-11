import React from 'react';

function EndGame({ restartGame, player}) {
    return (
        <div className="end-game-screen">
            {player !== "Empty" && <span className="win-text">{player === "O" ? 'O won' : 'X won'}</span>}
            {player === "Empty" && <span className="win-text">DRAW GAME</span>}

            <button className="btn" onClick={restartGame}>
                RESTART GAME
            </button>
        </div>
    );
}
export default EndGame;
