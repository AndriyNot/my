import React from 'react';
import { observer } from 'mobx-react-lite';
import authStore from '../auth/authStore';

const LoginButton: React.FC = observer(() => {
  const handleLogin = () => {
    authStore.login();
  };

  const handleLogout = () => {
    authStore.logout();
  };

  return (
    <div>
      {authStore.user ? (
        <>
          <p>Welcome, {authStore.user.profile.name}</p>
          <button onClick={handleLogout}>Logout</button>
        </>
      ) : (
        <button onClick={handleLogin}>Login</button>
      )}
    </div>
  );
});

export default LoginButton;
