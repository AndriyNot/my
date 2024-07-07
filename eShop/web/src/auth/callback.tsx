import React, { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import authStore from "../auth/authStore";

const Callback: React.FC = () => {
  const navigate = useNavigate();

  useEffect(() => {
    authStore.completeLogin().then(() => {
      navigate("/");
    });
  }, [navigate]);

  return (
    <div>
      <div>Loading...</div>
    </div>
  );
};

export default Callback;
