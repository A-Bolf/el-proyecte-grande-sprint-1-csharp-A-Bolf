import LoginButton from "./LoginButton";
import logo from "../SitRep_logo.png";
import { fadeIn } from "react-animations";
import styled, { keyframes } from "styled-components";
const FadeInDiv = styled.div`
  animation: ${keyframes`${fadeIn}`} 2s;
`;
const Landing = () => {
  return (
    <FadeInDiv>
      <div className="landing">
        <img className="logo" src={logo} alt="" />
        <div className="landing-bg">
          <div className="landing-container">
            <h1 className="landing-title">SitRep</h1>
            <p className="landing-text">Project Management tool</p>
          </div>
        </div>
        <LoginButton />
      </div>
    </FadeInDiv>
  );
};

export default Landing;
