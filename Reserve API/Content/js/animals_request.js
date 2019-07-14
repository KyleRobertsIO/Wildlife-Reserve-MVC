fetch('/api/animals')
    .then(function(response){
        return response.json();
    })
    .then(function (myJson) {
        let animals = JSON.stringify(myJson);
        localStorage.setItem('animals', animals);
        createAnimalCards();
    });

function createAnimalCards() {
    let data = localStorage.getItem('animals');
    if (data != null) {
        let animals = JSON.parse(data);
        console.log(animals);
        let template = document.getElementById("animal-card-template").content;
        let gridContainer = document.getElementById("animal-card-list");
        animals.forEach((animal) => {
            let clone = template.cloneNode(true);
            clone.querySelector('.animal-card-container').id = animal.SpeciesID;
            let filePath = `../Content/assets/images/animal_images/${animal.Photos[0].FileName}`;
            clone.querySelector('.card-image').setAttribute("src", filePath);
            clone.querySelector('.card-image').setAttribute("alt", `${animal.Species} Photo`);
            clone.querySelector('.animal-card-species').innerText = animal.Species;
            clone.querySelector('.animal-card-desc').innerText = animal.Description.DescriptionShort;
            gridContainer.appendChild(clone);
        });
    }
}

function displayAnimalPage() {
    console.log("Open Display");

}