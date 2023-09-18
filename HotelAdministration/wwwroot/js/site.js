document.addEventListener("DOMContentLoaded", function () {
    fetch('/Bookings/GetRoomBookings')
        .then((response) => response.json())
        .then((data) => {
            const roomContainer = document.getElementById('calendar');
            for (let roomNumber = 1; roomNumber <= 100; ++roomNumber) {
                const cardDiv = document.createElement('div');
                cardDiv.className = 'card mb-3';
                const cardBody = document.createElement('div');
                cardBody.className = 'card-body-calendar';
                cardDiv.appendChild(cardBody);
                const cardTitle = document.createElement('h5');
                cardTitle.className = 'card-title';
                cardTitle.textContent = `Room ${roomNumber}`;
                cardBody.appendChild(cardTitle);
                // Find bookings for the current room
                const roomBookings = data.filter((booking) => {
                    // Extract the room number from the title
                    const roomNumberFromTitle = booking.title.match(/Room (\d+)/);

                    // Check if the extracted room number matches the current room number
                    return roomNumberFromTitle && parseInt(roomNumberFromTitle[1]) === roomNumber;
                });

                if (roomBookings.length > 0) {
                    // If there are bookings, add them to the card
                    roomBookings.forEach((booking) => {
                        const bookingInfo = document.createElement('p');
                        bookingInfo.className = 'card-text';
                        const startDate = new Date(booking.start);
                        const endDate = new Date(booking.end);
                        const nameClient = booking.title;
                        console.log(booking);
                        const formattedStartDate = `${startDate.getDate()}/${startDate.getMonth() + 1}/${startDate.getFullYear()}`;
                        const formattedEndDate = `${endDate.getDate()}/${endDate.getMonth() + 1}/${endDate.getFullYear()}`;
                        bookingInfo.innerHTML = `<strong>🙌Check In:</strong> ${formattedStartDate}<strong> ➡      👋Check out:</strong> ${formattedEndDate} ` ;
                        cardBody.appendChild(bookingInfo);
                    });
                } else {
                    const bookingInfo = document.createElement('p');
                    bookingInfo.className = 'card-text';
                    bookingInfo.innerHTML = `No booking for this room`;
                    cardBody.appendChild(bookingInfo);
                }

                // Append the card to the room container
                roomContainer.appendChild(cardDiv);
            }
        })
        .catch((error) => console.error(error));
});