var express=require('express');
var app = express();

app.use('/',function(req, res, next) {
  res.header("Access-Control-Allow-Origin", "*");
  res.header("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
  next();
});

var server = require('http').Server(app);
var seats = [];
var Clients = [];
const io = require('socket.io')(server, {
  cors: {
    origin: '*',
  }
});

//Whenever someone connects this gets executed
io.on('connection', function (client) {
    console.log(client.id +' connected');

    //Whenever someone disconnects this piece of code executed
    client.on('join', function (data) {
        console.log(data);
        console.log(seats);
        console.log(Clients);

        Clients.push(client.id);
        seats.push(-1);

        client.emit('load_ghe_da_chon',seats);
    });

    client.on('Client-to-server-to-all', function (data) {
        console.log(client.id+' send to all: ' + data.key + ' : ' + data.value);
        switch (data.key) {
            case 'chon_ghe':
                seats[Clients.indexOf(client.id)]=data.value;
                break;
            case 'huydachon':
                delete seats[Clients.indexOf(client.id)];
                break;
        }
        console.log(seats);
        client.broadcast.emit(data.key, data.value);
    });

    client.on('Client-to-server', function (data) {
        console.log('Client send: '+data.key + ' : ' + data.value);
        client.emit(data.key, data.value);
    });

    client.on('disconnect', function (reason) {
        delete seats[Clients.indexOf(client.id)];
        delete Clients[Clients.indexOf(client.id)];
        client.broadcast.emit('load_ghe_da_chon', seats);
        console.log(client.id + ' user disconnected: ' + reason);
        console.log(seats);
    });
});

server.listen(3000, function () {
    console.log('listening on *:3000');
});