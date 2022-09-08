import { useState } from "react";
import Collapse from "@mui/material/Collapse";
import Login from "./Login/Login";
import Register from "./Registration/Register";
import Stack from "@mui/material/Stack";
const LoginButton = () => {
  const [open, setOpen] = useState(false);
  const [showRegister, setShowRegister] = useState(false);
  return (
    <>
      <Stack direction="row" spacing={5}>
        <button
          className="btn btn-primary"
          onClick={() => {
            setOpen(!open);
            if (showRegister) {
              setShowRegister(false);
            }
          }}
        >
          {open ? "Nevermind" : "Login"}
        </button>
        <button
          className="btn btn-primary"
          onClick={() => {
            setShowRegister(!showRegister);
            if (open) {
              setOpen(false);
            }
          }}
        >
          {showRegister ? "Nevermind" : "Register"}
        </button>
      </Stack>
      <Collapse in={open}>
        <Login />
      </Collapse>
      <Collapse in={showRegister}>
        <Register />
      </Collapse>
    </>
  );
};
export default LoginButton;
