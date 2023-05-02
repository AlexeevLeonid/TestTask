"use strict";


function setPostButton(connection) {
    
    document.getElementById("send").addEventListener("click", async () => {
        const id = document.getElementById("soccerId").value;
        const name = document.getElementById("name").value;
        const surname = document.getElementById("surname").value;
        const sex = document.getElementById("sex").value;
        const date = document.getElementById("date").value;
        const team = document.getElementById("teamInput").value;
        const country = document.getElementById("country").value;
        if (isEmpty(name) || isEmpty(surname) || isEmpty(sex) || isEmpty(date) || isEmpty(team) || isEmpty(country))
            alert("Одно из полей пустое")
        else {
            if (id === "") {
                await postSoccer(name, surname, sex, date, team, country, connection);
            }
            else {
                await putSoccer(id, name, surname, sex, date, team, country, connection);
            }
        }

    });
}

function isEmpty(str) {
    if (str.trim() == '')
        return true;
    return false;
}

async function postSoccer(iname, isurname, isex, idate, iteam, icountry, connection) {
    const response = await fetch("/soccer", {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            id: "1",
            name: iname,
            surname: isurname,
            sex: isex,
            date: idate,
            team: iteam,
            country: icountry
        })
    });
    if (response.status === 200) {
        await connection.invoke("Refresh").catch(function (err) {
            return console.error(err.toString());
        });
        alert("Футболист успешно записан")
    }
    else {
        const error = await response.json();
        alert(error);
    }
}

async function putSoccer(iid, iname, isurname, isex, idate, iteam, icountry, connection) {
    const response = await fetch("/soccer", {
        method: "PUT",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            id: iid,
            name: iname,
            surname: isurname,
            sex: isex,
            date: idate,
            team: iteam,
            country: icountry
        })
    });
    if (response.ok === true) {
        await connection.invoke("Refresh").catch(function (err) {
            return console.error(err.toString());
        });
        alert("Данные футболиста успешно изменены")
    }
    else {
        const error = await response.json();
        console.log(error.message);
    }
}

async function setSelectValues(selectName, apiUrl) {
    var select = document.getElementById(selectName);
    await fetch(apiUrl, {
        method: "GET",
        headers: { "Accept": "application/json" }
    }).then((response) => response.json()).then((data) => {
        for (const team of data) {
            select.options[select.options.length] = new Option(team, team);
        };
    })
}

async function startup(connection) {
    document.getElementById("teamSelect").options[document.getElementById("teamSelect").options.length] = new Option('', '');
    setSelectValues("sex", "/sex")
    setSelectValues("teamSelect", "/teams")
    setSelectValues("country", "/countries")
    setPostButton(connection);
    OnSelectTeam();
    const query = window.location.search;
    if (query.length > 0) {
        GetAndSetSelectedUser(query)
    } else
        document.getElementById("formTitle").textContent = "Добавление нового футболиста";
}

async function GetAndSetSelectedUser(query) {
    await fetch("/soccer" + query, {
        method: "GET",
        headers: { "Accept": "application/json" }
    }).then((response) => response.json()).then((data) => {
        if (!data.hasOwnProperty('message')) {
            document.getElementById("formTitle").textContent = "Изменение существующего футболиста";
            document.getElementById("soccerId").value = data.id;
            document.getElementById("name").value = data.name;
            document.getElementById("surname").value = data.surname;
            document.getElementById("sex").value = data.sex;
            document.getElementById("date").value = data.date;
            document.getElementById("teamInput").value = data.team;
            document.getElementById("teamSelect").value = data.team;
            document.getElementById("country").value = data.country;
        }
        else alert(data.message)
    })
}

function OnSelectTeam() {
    var select = document.getElementById("teamSelect");
    var input = document.getElementById("teamInput");
    select.addEventListener("change", function () {
        input.value = this.options[this.selectedIndex].text;
    });
}




var connection = new signalR.HubConnectionBuilder().withUrl("/refresh").build();
connection.start();
startup(connection);
//document.getElementById("send").addEventListener("click", function (event) {
//    connection.invoke("Refresh").catch(function (err) {
//        return console.error(err.toString());
//    });
//    event.preventDefault();
//});