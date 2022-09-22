import SideNavBar from "./SideNavBar";
import TopNavBar from "./TopNavBar";
import ReusableModal from "./Modal/ReusableModal";

const Layout = () => {
  return (
    <>
      <ReusableModal />
      <SideNavBar />
      <TopNavBar />
    </>
  );
};

export default Layout;
