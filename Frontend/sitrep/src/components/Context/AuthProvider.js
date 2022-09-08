import React, { createContext, useState } from "react";
import axios from "axios";
import { API_ENDPOINT } from "../../App";
import { useNavigate } from "react-router";

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  let navigate = useNavigate();
  const [auth, setAuth] = useState({});
  const [errMsg, setErrMsg] = useState("");

  const registerFetch = (data) => {
    if (data.get("password") === "" || data.get("username") === "") {
      setErrMsg("All Fields Are Required");
      return;
    }
    if (data.get("password") !== data.get("password-confirm")) {
      setErrMsg("Password Fields Must Match");
      return;
    }
    axios
      .post(`${API_ENDPOINT}/api/Auth/register`, {
        userName: data.get("username"),
        password: data.get("password"),
      })
      .then((res) => {
        console.log(res.data);
        loginFetch(data.get("username"), data.get("password"));
      })
      .catch((error) => {
        if (error.response) {
          console.log(error);
          setErrMsg("Username Already Taken");
        }
      });
  };

  const logOut = () => {
    setAuth({});
    navigate("/");
  };

  const loginFetch = (username, password) => {
    axios
      .post(`${API_ENDPOINT}/api/Auth/login`, {
        userName: username,
        password: password,
      })
      .then((res) => {
        console.log(res.data);
        let accessToken = res?.data;
        setAuth({ username, password, accessToken });
        sessionStorage.user = JSON.stringify({
          username,
          password,
          accessToken,
        });
        navigate("/app/dashboard");
      })
      .catch((error) => {
        if (!error.response) {
          console.log(error);
          setErrMsg("No Server Response");
        } else if (error.response.status === 401) {
          setErrMsg("Unauthorized");
        } else if (error.response.status === 400) {
          setErrMsg("User Not Found!");
        } else {
          setErrMsg("Login Failed");
        }
      });
  };

  return (
    <AuthContext.Provider
      value={{
        auth,
        setAuth,
        logOut,
        loginFetch,
        registerFetch,
        errMsg,
        setErrMsg,
      }}
    >
      {children}
    </AuthContext.Provider>
  );
};
export default AuthContext;
