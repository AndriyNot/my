import React from 'react';
import userManager from '../services/userManager';

const SilentRenew = () => {
    userManager.signinSilentCallback();
    return <div>Silent Renew</div>;
};

export default SilentRenew;