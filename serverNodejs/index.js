var express=require('express');
var app = express();

app.use('/',function(req, res, next) {
  res.header("Access-Control-Allow-Origin", "*");
  res.header("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
  next();
});

var server = require('http').Server(app);
var listShowTimeId = [];
var Clients = [];
const io = require('socket.io')(server, {
  cors: {
    origin: '*',
  }
});

const bookticket = io.of('/bookticket');

bookticket.on('connection', function (client) {
    client.on('Join room', function (data) {
        var room = data.idLichChieu;
        client.join(room);
        console.log(client.id + " :vừa gia nhập " + room);

        Clients.push(client.id);
        listShowTimeId.push({ id: room, idGhe: -1 });

        console.log(listShowTimeId);

        client.emit('load_ghe_da_chon', listShowTimeId.filter(val => val.id == room));

    });

    client.on('Client-to-server-to-all', function (data) {
        console.log(client.id + ' send to all: ' + data.key + ' : ' + data.idGhe);
        console.log(data);
        switch (data.key) {
            case 'chon-ghe':
                chonGhe(client.id, data.idGhe);
                break;
            case 'huy-ghe-da-chon':
                huyGhe(client.id);
                break;
        }

        console.log(listShowTimeId);
        console.log(Clients);
        var room = data.idLichChieu;

        client.to(data.idLichChieu).emit('load_ghe_da_chon', listShowTimeId.filter(val => val.id == room));
        client.emit('load_ghe_da_chon', listShowTimeId.filter(val => val.id == room));

    });

    client.on('disconnect', function (reason) {
        console.log(client.id + ' user disconnected: ' + reason);

        var index = Clients.indexOf(client.id);
        
        var room = listShowTimeId[index].id;
        console.log(room);
        Clients = arrRemove(Clients, index);
        listShowTimeId = arrRemove(listShowTimeId, index);

        console.log(Clients);
        client.broadcast.emit('load_ghe_da_chon', listShowTimeId.filter(val => val.id == room));

        console.log(listShowTimeId); console.log(Clients);
    });
});

server.listen(process.env.PORT || 3000, function () {
    console.log('listening on *:3000');
});

function arrRemove(array, index) {
    return array.filter(function (val) { return val != array[index]; });
}

function chonGhe(clientId, data) {

    console.log('Chon Ghe:');
    var index = Clients.indexOf(clientId);
    listShowTimeId[index].idGhe = data;
    
}

function huyGhe(clientId) {

    console.log('Huy Ghe:');
    var index = Clients.indexOf(clientId);
    listShowTimeId[index].idGhe = -1;

}

//data={idLichChieu,idGhe}
//listShowTimeId=[idLichChieu:{id,idGhe}]