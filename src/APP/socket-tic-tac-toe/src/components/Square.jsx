import React from 'react';

function Square({ boardElements,sendClick,placeFigure, userFigure,turn,gameFinished}) {
    
    const handleClickNew = (position) => {
        
        if(userFigure != turn || gameFinished)
            return

        sendClick(position);
        placeFigure(position,userFigure);
    }
    
    
    return (
        <div className="board">
            {Object.keys(boardElements).map((item, index) => {
                if (boardElements[item] === 'Empty') {
                    return (
                        <div key={index} className="square" onClick={() => handleClickNew(item)} />
                    );
                } else {
                    return (
                        <div key={index} className="square clicked">
                            {boardElements[item]}
                        </div>
                    );
                }
            })}
        </div>
    );
}

export default Square;
