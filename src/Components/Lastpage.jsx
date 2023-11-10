import React, { useEffect, useState } from "react";
import GoogleMapReact from "google-map-react";
import { IoIosLogOut } from "react-icons/io";

const DEFAULT_LATITUDE = 40.7128;
const DEFAULT_LONGITUDE = -74.006;

const Lastpage = () => {
  const [userLocation, setUserLocation] = useState({
    lat: null,
    lng: null,
  });

  useEffect(() => {
    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition((position) => {
        setUserLocation({
          lat: position.coords.latitude,
          lng: position.coords.longitude,
        });
      });
    } else {
      console.log("Geolocation is not supported by this browser.");
    }
  }, []);

  return (
    <div>
      <div style={{ height: "400px", width: "100%" }}>
        <div className="logout">
          <button>
            Logout <IoIosLogOut />
          </button>
        </div>

        <GoogleMapReact
          bootstrapURLKeys={{
            key: "YOUR_GOOGLE_MAPS_API_KEY",
          }}
          defaultCenter={{
            lat: userLocation.lat || DEFAULT_LATITUDE,
            lng: userLocation.lng || DEFAULT_LONGITUDE,
          }}
          defaultZoom={15}
        >
          {userLocation.lat && userLocation.lng && (
            <Marker
              lat={userLocation.lat}
              lng={userLocation.lng}
              text="You are here"
            />
          )}
        </GoogleMapReact>
      </div>

      <div>
        <h2>Your Current Location:</h2>
        {userLocation.lat && userLocation.lng && (
          <p>
            Latitude: {userLocation.lat.toFixed(6)}, Longitude:{" "}
            {userLocation.lng.toFixed(6)}
          </p>
        )}
      </div>
    </div>
  );
};

const Marker = ({ text }) => <div>{text}</div>;

export default Lastpage;
