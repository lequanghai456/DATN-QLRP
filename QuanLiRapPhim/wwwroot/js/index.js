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

const bookticket=io.of('/bookticket');
//Whenever someone connects this gets executed
bookticket.on('connection', function (client) {
    console.log(client.id +' connected');

    //Whenever someone disconnects this piece of code executed
    client.on('Join room', function (data) {
        client.join(data.idLichChieu);
        console.log(client.id + " :vừa gia nhập " + data.idLichChieu);

        Clients.push(client.id);
        seats.push(-1);

        console.log(seats);
        console.log(Clients);

        client.to(data.idLichChieu).emit('load_ghe_da_chon', seats);
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

        console.log(seats); console.log(Clients);
        client.to().broadcast.emit('load_ghe_da_chon', seats);
        client.emit('load_ghe_da_chon', seats);
    });

    client.on('disconnect', function (reason) {
        seats = arrRemove(seats, Clients.indexOf(client.id));
        Clients = arrRemove(Clients, Clients.indexOf(client.id));
        
        client.broadcast.emit('load_ghe_da_chon', seats);
        console.log(client.id + ' user disconnected: ' + reason);

        console.log(seats); console.log(Clients);
    });
});

server.listen(3000, function () {
    console.log('listening on *:3000');
});

function arrRemove(array, index) {
    return array.filter(function (val) { return val != array[index]; });
}