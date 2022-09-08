import { useRef, useState, useEffect, useContext } from "react";
import * as React from "react";
import AuthContext from "../Context/AuthProvider";
import Button from "@mui/material/Button";
import ErrorAlert from "../Alerts/ErrorAlert";
import CssBaseline from "@mui/material/CssBaseline";
import TextField from "@mui/material/TextField";
import Link from "@mui/material/Link";
import Grid from "@mui/material/Grid";
import Box from "@mui/material/Box";
import Container from "@mui/material/Container";
import { createTheme, ThemeProvider } from "@mui/material/styles";

const theme = createTheme({
  palette: {
    primary: { main: "#343a40" },
  },
});

const Login = () => {
  const { loginFetch, errMsg, setErrMsg } = useContext(AuthContext);

  const userRef = useRef();
  const [user, setUser] = useState("");
  const [pwd, setPwd] = useState("");

  //focus username(?) field on page render
  useEffect(() => {
    userRef.current.focus();
  }, []);

  //clear error msg when user/pwd fields change
  useEffect(() => {
    setErrMsg("");
  }, [user, pwd]);

  const handleSubmit = (event) => {
    event.preventDefault();
    if (
      userValidationRef.current.reportValidity() &&
      pwValidationRef.current.reportValidity()
    ) {
      const data = new FormData(event.currentTarget);
      console.log({
        username: data.get("username"),
        password: data.get("password"),
      });
      loginFetch(user, pwd);
      setUser("");
      setPwd("");
    } else {
      setErrMsg("Not all fields were filled out");
    }
  };

  const userValidationRef = useRef();
  const pwValidationRef = useRef();
  return (
    <ThemeProvider theme={theme}>
      <Container component="main" maxWidth="xs">
        <CssBaseline />
        <Box
          sx={{
            display: "flex",
            flexDirection: "column",
            alignItems: "center",
          }}
        >
          <Box
            component="form"
            onSubmit={handleSubmit}
            noValidate
            sx={{ mt: 1 }}
          >
            <TextField
              margin="normal"
              required
              fullWidth
              ref={userRef}
              inputRef={userValidationRef}
              id="username"
              label="Username"
              name="username"
              value={user}
              onChange={(e) => setUser(e.target.value)}
              autoComplete="off"
              autoFocus
            />
            <TextField
              margin="normal"
              required
              fullWidth
              inputRef={pwValidationRef}
              name="password"
              label="Password"
              type="password"
              id="password"
              onChange={(e) => setPwd(e.target.value)}
              value={pwd}
              autoComplete="off"
            />
            <Button
              type="submit"
              fullWidth
              variant="contained"
              sx={{ mt: 3, mb: 2 }}
            >
              Sign In
            </Button>
            <ErrorAlert text={errMsg} open={errMsg ? true : false} />

            <Grid container>
              <Grid item xs>
                <Link
                  href="https://letmegooglethat.com/?q=Memory+improvement+techniques"
                  variant="body2"
                >
                  Forgot password?
                </Link>
              </Grid>
              <Grid item></Grid>
            </Grid>
          </Box>
        </Box>
      </Container>
    </ThemeProvider>
  );
};
export default Login;
