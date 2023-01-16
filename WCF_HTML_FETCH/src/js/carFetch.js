let pageCount = 15;
let maxCount;
const prevButton = document.querySelector(".prev-page");
const nextButton = document.querySelector(".next-page");
const searchButton = document.querySelector(".searchButton");
const searchInput = document.querySelector(".searchinput");

searchButton.addEventListener("click", () => {
  if (searchInput.value == "") {
    populateTable();
    updatePageCount();
    return;
  }
  populateTable(searchInput.value);
});

prevButton.addEventListener("click", () => {
  pageCount >= 30 ? pageCount -= 15 : pageCount;
  if (maxCount < 16) return;
  if (searchInput.value) {
    populateTable(searchInput.value);
    updatePageCount();
    return;
  }
  populateTable();
  updatePageCount();
});

nextButton.addEventListener("click", () => {
  pageCount < maxCount ? pageCount += 15 : pageCount;
  if (maxCount < 16) return;
  if (searchInput.value) {
    populateTable(searchInput.value);
    updatePageCount();
    return;
  }
  populateTable();
  updatePageCount();
});

populateTable();

function updatePageCount() {
  document.querySelector(".page").innerHTML = `Page ${pageCount / 15}`;
}

function fetchCarDatabase() {
  return fetch(`http://localhost:3000/CarListDB`,{method:'GET'})
    .then(response => response.json())
    .then(data => {
      return data;
    });
}

function onCarSpecifiedSearch(name) {
  return fetch(`http://localhost:3000/CarListDBSpec?Name=${name}`,{method:'GET'})
    .then(response => response.json())
    .then(data => {
      return data;
  });
}

function onClickDelete(event) {
  let row = event.parentNode.parentNode;
  let cells = row.getElementsByTagName("td");
  onCarSpecifiedDelete(cells[0].innerHTML);
  populateTable();
}

function onCarSpecifiedDelete(id) {
  fetch(`http://localhost:3000/OneCarDelete?ID=${id}`, {method: 'DELETE'})
  .then(response => response.json())
  .then(data => console.log(data));
}

function onClickUpdate(event) {
  let row = event.parentNode.parentNode;
  let cells = row.getElementsByTagName("td");
  onCarUpdate(cells[0].innerHTML)
}

function onCarUpdate(id) {
  let car = {
    "car" : {
      ID: id,
      Make: document.querySelector(`.carMake${id}`).value,
      Model: document.querySelector(`.carModel${id}`).value,
      Year: document.querySelector(`.carYear${id}`).value,
      Color: document.querySelector(`.carColor${id}`).value,
      Vin: document.querySelector(`.carVin${id}`).value
    }
  };
  fetch('http://localhost:3000/CarPutDB', {
    method: 'PUT',
    body: JSON.stringify(car),
    headers: {
      'Content-Type': 'application/json'
    }
  })
  .then(response => response.json())
  .then(data => console.log(data));
}

function onCarPost() {
  let make = document.querySelector("#post1");
  let model = document.querySelector("#post2");
  let year = document.querySelector("#post3");
  let color = document.querySelector("#post4");
  let vin = document.querySelector("#post5");
  if (make.value == "" || model.value == "" || year.value == "" || TryParseInt(year.value, 0) == 0 || color.value == "" || vin.value == "") return;

  let car = {
    "car" : {
      ID: 1,
      Make: make.value,
      Model: model.value,
      Year: year.value,
      Color: color.value,
      Vin: vin.value
    }
  };

  fetch('http://localhost:3000/CarPostDB', {
    method: 'POST',
    body: JSON.stringify(car),
    headers: {
      'Content-Type': 'application/json'
    }
  })
  .then(response => response.json())
  .then(data => console.log(data));
}

function generateTableRow(id, carMake, carModel, carYear, color, carVin) {
  let row = `<tr>
    <td>${id}</td>
    <td ondblclick="makeInputFields(this)">${carMake}</td>
    <td ondblclick="makeInputFields(this)">${carModel}</td>
    <td ondblclick="makeInputFields(this)">${carYear}</td>
    <td ondblclick="makeInputFields(this)">${color}</td>
    <td ondblclick="makeInputFields(this)">${carVin}</td>
    <td>
      <button class="update-btn" onclick="onClickUpdate(this)" disabled>Update</button>
      <button class="delete-btn" onclick="onClickDelete(this)">Delete</button>
    </td>
  </tr>`;
  return row;
}

function TryParseInt(str,defaultValue) {
  var retValue = defaultValue;
  if(str !== null) {
      if(str.length > 0) {
          if (!isNaN(str)) {
              retValue = parseInt(str);
          }
      }
  }
  return retValue;
}

async function populateTable(name) {
  let cars;
  if (name === undefined) {
    cars = await fetchCarDatabase();
  } else {
    cars = await onCarSpecifiedSearch(name);
  }
  maxCount = cars.length;
  document.querySelector(".cars").innerHTML = "";
  let max = pageCount > cars.length ? cars.length : pageCount;
  for (let index = pageCount - 15; index < max; index++) {
    let row = generateTableRow(cars[index].ID, cars[index].Make, cars[index].Model, cars[index].Year, cars[index].Color, cars[index].Vin);
    document.querySelector(".cars").innerHTML += row;
  }
  addLastPostRow(cars.length);
}

function makeInputFields(event) {
  let sizes = [2, 9, 13, 1, 6, 18];
  let names = ["carMake", "carModel", "carYear", "carColor", "carVin"];
  let row = event.parentNode;
  let cells = row.getElementsByTagName("td");

  for (let i = 1; i < 6; i++) {
    let cell = cells[i];
    let input = document.createElement("input");
    input.type = "text";
    input.value = cell.innerHTML;
    input.size = sizes[i];
    input.classList.add(`${names[i - 1]}${cells[0].innerHTML}`);
    cell.innerHTML = "";
    cell.appendChild(input);
    cell.removeAttribute("ondblclick");
  }
  let updateButton = event.parentNode.getElementsByTagName("button")[0];
  updateButton.removeAttribute("disabled");
}

function addLastPostRow(length) {
  let row = `<tr>
    <td>${length + 2}</td>
    <td><input size="9" id="post1"></td>
    <td><input size="13" id="post2"></td>
    <td><input size="1" id="post3"></td>
    <td><input size="6" id="post4"></td>
    <td><input size="18" id="post5"></td>
    <td>
      <button class="send-btn" onclick="onCarPost()">Submit</button>
    </td>
  </tr>`;
  document.querySelector(".cars").innerHTML += row;
}