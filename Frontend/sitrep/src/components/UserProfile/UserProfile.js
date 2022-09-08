import React from "react";
import Avatar from "@mui/material/Avatar";
import Stack from "@mui/material/Stack";
import Box from "@mui/material/Box";
import TextField from "@mui/material/TextField";
import Button from "@mui/material/Button";
import axios from "axios";
import { API_ENDPOINT } from "../../App";

const UserProfile = () => {
  console.log(sessionStorage);
  const user = JSON.parse(sessionStorage.user) || {};
  const changePassword = (user) => {
    const userdto = { username: user.username, password: user.password };

    axios
      .post(`${API_ENDPOINT}/api/Auth/UpdatePassword`, userdto)
      .then((res) => console.log(res))
      .catch((error) => console.log(error));
  };

  const handleSubmit = (event) => {
    event.preventDefault();
    const data = new FormData(event.currentTarget);
    if (data.get("password-old") !== user.password) {
    } else if (data.get("password-new") !== data.get("password-new-confirm")) {
    } else {
      let tempuser = user;
      tempuser.password = data.get("password-new");
      changePassword(tempuser).then(() => {
        user.password = tempuser.password;
        sessionStorage.user = JSON.stringify(user);
      });
    }
  };
  return (
    <Box component="form" onSubmit={handleSubmit} noValidate sx={{ mt: 1 }}>
      <Stack spacing={3} justifyContent="center" alignItems="center">
        <Avatar />
        {user.username || "not logged in"}
      </Stack>
      Change Password:
      <Stack spacing={2}>
        <TextField
          required
          id="password-old"
          name="password-old"
          label="Old Password"
          type="password"
          variant="filled"
        />
        <TextField
          required
          id="password-new"
          name="password-new"
          label="New Password"
          type="password"
          variant="filled"
        />
        <TextField
          required
          id="password-new-confirm"
          name="password-new-confirm"
          label="Confirm Password"
          type="password"
          variant="filled"
        />
        <Button type="submit" fullWidth variant="contained">
          Change Password
        </Button>
      </Stack>
    </Box>
  );
};

export default UserProfile;
