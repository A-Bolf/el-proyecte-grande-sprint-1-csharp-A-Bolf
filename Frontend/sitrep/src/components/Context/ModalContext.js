import React, { createContext, useState } from "react";

const ModalContext = createContext({});

export const ModalProvider = ({ children }) => {
  const [showModal, setShowModal] = useState(false);
  const [modalContent, setModalContent] = useState({});
  const toggleModal = () => {
    setShowModal(!showModal);
  };
  return (
    <ModalContext.Provider
      value={{
        showModal,
        setShowModal,
        toggleModal,
        modalContent,
        setModalContent,
      }}
    >
      {children}
    </ModalContext.Provider>
  );
};
export default ModalContext;
