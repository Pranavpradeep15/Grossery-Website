import React, { useEffect, useState } from "react";
import "./Dashboard.css";
import { IoIosLogOut } from "react-icons/io";
import { MdNavigateNext } from "react-icons/md";
import { Link } from "react-router-dom";
import axios from "axios";

const Dashboard = () => {
  const [data, setData] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await axios.get("https://localhost:7289/api/User");
        setData(response.data.value);
        console.log(response);
      } catch (error) {
        console.error("Error fetching data:", error);
      }
    };

    fetchData();
  }, [setData, axios]);

  return (
    <div className="dash-container">
      <div className="logout">
        <button>
          Logout <IoIosLogOut />
        </button>
      </div>
      <div className="scrolltable">
        <div>
          <table>
            <thead>
              <tr>
                <th>Vegetables</th>
                <th>Price</th>
              </tr>
            </thead>
            <tbody>
              {data.map((item) => (
                <tr key={item.vegName}>
                  <td>{item.vegName}</td>
                  <td>
                    <input type="text" />
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>
      <div className="save">
        <button>Save</button>
      </div>
      <div className="cache">
        <h1>
          The user cookie for this session is : <input type="text" />
        </h1>
      </div>
      <div className="track">
        <Link to="/Lastpage">
          <button>
            Track User
            <MdNavigateNext />
          </button>
        </Link>
      </div>
    </div>
  );
};

export default Dashboard;
