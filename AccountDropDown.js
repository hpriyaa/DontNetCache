import React, { useState, useEffect } from 'react';
import Form from 'react-bootstrap/Form';

function AccountDropdown({ onSelectAccount }) {
    const [accounts, setAccounts] = useState([]);
    const [selectedAccount, setSelectedAccount] = useState('');

    useEffect(() => {
        // Fetch the list of accounts from your C# API (replace with your API endpoint)
        fetch('/api/accounts')
            .then(response => response.json())
            .then(data => {
                setAccounts(data);
            })
            .catch(error => console.error('Error fetching accounts:', error));
    }, []);

    const handleAccountChange = (event) => {
        const selectedValue = event.target.value;
        setSelectedAccount(selectedValue);
        onSelectAccount(selectedValue);
    };

    return (
        <Form.Group>
            <Form.Label>Select an Account</Form.Label>
            <Form.Control
                as="select"
                value={selectedAccount}
                onChange={handleAccountChange}
            >
                <option value="">Select an account</option>
                {accounts.map(account => (
                    <option key={account.id} value={account.id}>
                        {account.accountName}
                    </option>
                ))}
            </Form.Control>
        </Form.Group>
    );
}

export default AccountDropdown;
