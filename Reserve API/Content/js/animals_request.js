fetch('/api/animals')
    .then(function(response){
        return response.json();
    })
    .then(function (data) {
        let animals = JSON.stringify(data);
        localStorage.setItem('animals', animals);
        createAnimalCards();
    });

var swiper = null;

function createAnimalCards() {
    let data = localStorage.getItem('animals');
    document.getElementById("loading-screen-container").style.display = "none";
    if (data != null) {
        let animals = JSON.parse(data);
        let template = document.getElementById("animal-card-template").content;
        let gridContainer = document.getElementById("animal-card-list");
        animals.forEach((animal) => {
            let clone = template.cloneNode(true);
            let cardContainer = clone.querySelector('.animal-card-container');
            cardContainer.id = animal.SpeciesID;
            let filePath = `../Content/assets/images/animal_images/${animal.Photos[0].FileName}`;
            clone.querySelector('.card-image').setAttribute("src", filePath);
            clone.querySelector('.card-image').setAttribute("alt", `${animal.Species} Photo`);
            clone.querySelector('.animal-card-species').innerText = animal.Species;
            clone.querySelector('.animal-card-desc').innerText = animal.Description.DescriptionShort;
            if (sessionUsername != '') {
                clone.querySelector(".edit-animal-button").style.display = "flex";
                clone.querySelector(".edit-button-link").setAttribute("href", `/home/edit_animal/${animal.SpeciesID}`);
            }
            gridContainer.appendChild(clone);
        });
    }
}

function displayAnimalPage(speciesId) {
    let shortFormMonths = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];

    let animalsData = JSON.parse(localStorage.getItem('animals'));
    let animalObj = null;
    animalsData.forEach((animal) => {
        if (speciesId == animal.SpeciesID) {
            animalObj = animal;
        }
    });

    window.scrollTo(0,0);

    let swiperWrapper = document.getElementById("image-roll");
    animalObj.Photos.forEach((photo) => {
        let slide = document.createElement("div");
        slide.classList.add("swiper-slide");
        slide.style.backgroundImage = `url(../Content/assets/images/animal_images/${photo.FileName})`;
        swiperWrapper.appendChild(slide);
    });

    document.getElementById("detail-animal-species").innerText = animalObj.Species;
    document.getElementById("detail-animal-description").innerText = animalObj.Description.DescriptionLong;
    document.getElementById("detail-animal-population").innerText = animalObj.Population;

    let startDate = new Date(animalObj.Description.BreedingSeasonStart);
    let startMonth = shortFormMonths[startDate.getMonth()];
    let endDate = new Date(animalObj.Description.BreedingSeasonEnd);
    let endMonth = shortFormMonths[endDate.getMonth()];
    document.getElementById('detail-animal-breeding-season').innerText = `${startMonth} - ${endMonth}`;

    swiper = new Swiper('.swiper-container', {
        effect: 'coverflow',
        grabCursor: true,
        centeredSlides: true,
        slidesPerView: 'auto',
        observer: true,
        observeParents: true,
        speed: 2000,
        coverflowEffect: {
            rotate: 45,
            stretch: 0,
            depth: 100,
            modifier: 1,
            slideShadows: true,
        },
        autoplay: {
            delay: 3000,
            disableOnInteraction: false,
        },
        pagination: {
            el: '.swiper-pagination',
        }
    });

    /* Display Details Page and remove Animal List Grid */
    let detailsContainer = document.getElementById("animal-details-page");
    let listContainer = document.getElementById("animal-card-list-container");
    detailsContainer.style.display = "block";
    listContainer.style.display = "none";
}

function closeDetails() {
    // Remove images from gallary
    swiper.slideTo(0, 1, false);
    let slider = document.getElementById("image-roll");
    slider.innerHTML = "";
    swiper = null;

    let detailsContainer = document.getElementById("animal-details-page");
    let listContainer = document.getElementById("animal-card-list-container");
    detailsContainer.style.display = "none";
    listContainer.style.display = "grid";
}