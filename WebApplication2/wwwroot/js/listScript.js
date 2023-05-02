"use strict";


async function startup() {
    document.getElementById("version").value = -1
    fillTable()
    await setSelectValues("sex", "/sex")
    await setSelectValues("team", "/teams")
    await setSelectValues("country", "/countries")
}

async function fillTable() {
    const response = await fetch("/soccers" + window.location.search, {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        const soccers = await response.json();
        const rows = document.getElementById("table");
        soccers.forEach(soccer => rows.append(row(soccer)));
    }
}

async function refresh() {
    clearTable();
    fillTable();
}

function row(soccer) {

    const tr = document.createElement("tr");
    tr.setAttribute("data-rowid", soccer.id);

    const nameTd = document.createElement("td");
    nameTd.append(soccer.name);
    tr.append(nameTd);

    const surnameTd = document.createElement("td");
    surnameTd.append(soccer.surname);
    tr.append(surnameTd);

    const sexTd = document.createElement("td");
    sexTd.append(soccer.sex);
    tr.append(sexTd);

    const dateTd = document.createElement("td");
    dateTd.append(soccer.date);
    tr.append(dateTd);

    const teamTd = document.createElement("td");
    teamTd.append(soccer.team);
    tr.append(teamTd);

    const countryTd = document.createElement("td");
    countryTd.append(soccer.country);
    tr.append(countryTd);

    const linksTd = document.createElement("td");

    const editLink = document.createElement("button");
    editLink.append("Изменить");
    editLink.addEventListener("click",
        () => window.location = "/?id=" + soccer.id
    )
    linksTd.append(editLink);
    tr.appendChild(linksTd);

    return tr;
}

async function setSelectValues(selectName, apiUrl) {
    var select = document.getElementById(selectName);
    select.options[select.options.length] = new Option('', '');
    await fetch(apiUrl, {
            method: "GET",
            headers: { "Accept": "application/json" }
    }).then((response) => response.json()).then((data) => {
            for (const record of data) {
                select.options[select.options.length] = new Option(record, record);
            };
        })
    let url = new URL(window.location);
    select.value = url.searchParams.get(selectName)
    select.addEventListener("change", function () {
        let url = new URL(window.location);
        select = document.getElementById(selectName)
        if (!isEmpty(select.options[select.selectedIndex].text)) {
            url.searchParams.set(selectName, select.options[select.selectedIndex].text);
        } else {
            url.searchParams.delete(selectName)
        }
        window.location = url;
    });
}

function isEmpty(str) {
    if (str.trim() == '')
        return true;
    return false;
}

function clearTable() {
    var element = document.getElementById("table")
    while (element.firstChild) {
        element.removeChild(element.firstChild);
    }
}

startup();
var connection = new signalR.HubConnectionBuilder().withUrl("/refresh").build();
connection.start();
connection.on("Refresh", function () {
    refresh();
});
    