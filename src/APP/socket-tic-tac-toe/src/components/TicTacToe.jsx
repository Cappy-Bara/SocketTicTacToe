import React, { useState } from 'react';
import Square from './Square';
import EndGame from './EndGame';

const signalR = require('@microsoft/signalr');

let connection = new signalR.HubConnectionBuilder()
    .withUrl('http://localhost:5209/play', {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets,
    })
    .build();

function TicTacToe() {

    const [PlayerShape, setPlayerShape] = useState("");
    const [Turn, setPlayerTurn] = useState("");
    const [Winner, setWinner] = useState();
    const [gameFinished, setGameFinished] = useState(false);


    const placeFigure = (position, shape) => {
        let temp = { ...board };
        temp[position] = shape;

        setBoard(temp);
        console.log(board)
    }

    connection.on('InitializePlayer', (data) => {
        console.log(`GOT SHAPE: ${data.shape}`)
        setPlayerShape(data.shape);
    });

    connection.on('NextTurn', (data) => {
        console.log(data.shape)
        setPlayerTurn(data.shape);
    });

    connection.on('FigurePlaced', (data) => {
        placeFigure(`(${data.posX},${data.posY})`, data.shape)
    });

    connection.on("GameFinished",(data) => {
        console.log(data.winner);
        setWinner(data.winner);
        setGameFinished(true);
    })
    
    connection.on("GameReseted",() => {
        console.log("GameReseted")
        restartedByAnotherPlayer();
    })

    if(connection.state !== "Connected"){
        connection.start()
        .then(() => console.log('CONNECTION INITIALIZED'));
    }


    function sendPosition(position) {
        let posX = Number(position[1]);
        let posY = Number(position[3]);

        let dataObject = {
            "PosX": posX,
            "PosY": posY,
            "Shape": PlayerShape,
        }

        connection.invoke('MakeMove', dataObject);
    }

    const initializeBoard = () => {
        const dict = {}

        for (let i = 0; i < 3; i++) {
            for (let j = 0; j < 3; j++) {
                dict[`(${i},${j})`] = "Empty";
            }
        }

        return dict;
    }
    const [board, setBoard] = useState(initializeBoard);

    function restartGame() {
        setGameFinished(false);
        setWinner(null);
        setBoard(initializeBoard());
        console.log("RESTART");
        connection.invoke('Reset');
    }

    function restartedByAnotherPlayer(){
        setGameFinished(false);
        setWinner(null);
        setBoard(initializeBoard());
    }

    return (
        <div>
            <span className="win-history">
                Playing As: {PlayerShape} <br /> Turn: {Turn}
            </span>
            {gameFinished && (
                <EndGame
                    restartGame={restartGame}
                    player={Winner}
                />
            )}
            <Square
                boardElements={board}
                gameFinished={gameFinished}
                sendClick={sendPosition}
                placeFigure={placeFigure}
                userFigure={PlayerShape}
                turn={Turn} />
        </div>
    );
}

export default TicTacToe;
