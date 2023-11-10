import React, { useState } from "react";
import "./Loginpage.css";
import { useNavigate } from "react-router-dom";
import axios from "axios";

const Loginpage = () => {
  const [Email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [errorMessage, setErrorMessage] = useState("");
  const navigate = useNavigate();

  const handleEmailChange = (e) => {
    setEmail(e.target.value);
  };

  const handlePasswordChange = (e) => {
    setPassword(e.target.value);
  };

  const handleLogin = async () => {
    try {
      const response = await axios.post(
        "https://localhost:7289/api/User/verify",
        {
          Email: Email,
          UserPassword: password,
        }
      );

      if (
        response.data === "Wrong Password" ||
        response.data === "Verification Failed"
      ) {
        setErrorMessage("Invalid email or password");
        console.log("Verification failed");
      } else {
        navigate("/Dashboard");
        console.log("Verification success");
      }
    } catch (error) {
      console.error("Error:", error);
      setErrorMessage("An error occurred. Please try again.");
    }
  };

  return (
    <div className="login-container">
      <div className="cover">
        <div className="cover-header">
          <h1>Please Login</h1>
        </div>
        <div className="cover-area">
          <div className="userpass user-input">
            <input
              type="text"
              value={Email}
              placeholder="EMAIL"
              onChange={handleEmailChange}
            />
          </div>
          <div className="userpass user-input">
            <input
              type="password"
              value={password}
              placeholder="PASSWORD"
              onChange={handlePasswordChange}
            />
          </div>
          <div className="user-action">
            <button className="cover-button" onClick={handleLogin}>
              Login
            </button>
          </div>
        </div>
      </div>
      <div>
        {errorMessage && <div className="error-message">{errorMessage}</div>}
      </div>
    </div>
  );
};

export default Loginpage;
