import React, { useState, useEffect } from 'react';

function AccountDropdown() {
    const [accounts, setAccounts] = useState([]);
    const [selectedAccount, setSelectedAccount] = useState('');

    useEffect(() => {
        // Fetch accounts from your C# API (replace with your API endpoint)
        fetch('/api/accounts')
            .then((response) => response.json())
            .then((data) => setAccounts(data))
            .catch((error) => console.error('Error fetching accounts:', error));
    }, []);

    const handleAccountSelect = (event) => {
        setSelectedAccount(event.target.value);
    };

    return (
        <div>
            <label htmlFor="accountDropdown">Select an Account:</label>
            <select
                id="accountDropdown"
                className="form-control"
                value={selectedAccount}
                onChange={handleAccountSelect}
            >
                <option value="">Select an account</option>
                {accounts.map((account) => (
                    <option key={account.id} value={account.id}>
                        {account.name}
                    </option>
                ))}
            </select>
        </div>
    );
}

export default AccountDropdown;
