  const [formData, setFormData] = useState({
    oldpin: '',
    newpin: '',
    confirmpin: '',
  });

  // Handle input changes and update the form data state
  const handleInputChange = (event) => {
    const { name, value } = event.target;
    setFormData({
      ...formData,
      [name]: value,
    });
  };

  // Handle form submission (you can add validation logic here)
  const handleSubmit = (event) => {
    event.preventDefault();

    // Access the values in formData
    const { oldpin, newpin, confirmpin } = formData;

    // Perform validation and further actions as needed
    if (newpin !== confirmpin) {
      alert('New PIN and Confirm PIN do not match.');
    } else {
      // Submit the form or perform other actions
      console.log('Form submitted with values:', formData);
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <div>
        <label htmlFor="oldpin">Old PIN</label>
        <input
          type="password"
          id="oldpin"
          name="oldpin"
          value={formData.oldpin}
          onChange={handleInputChange}
        />
      </div>
      <div>
        <label htmlFor="newpin">New PIN</label>
        <input
          type="password"
          id="newpin"
          name="newpin"
          value={formData.newpin}
          onChange={handleInputChange}
        />
      </div>
      <div>
        <label htmlFor="confirmpin">Confirm PIN</label>
        <input
          type="password"
          id="confirmpin"
          name="confirmpin"
          value={formData.confirmpin}
          onChange={handleInputChange}
        />
      </div>
      <button type="submit">Submit</button>
    </form>
  );
}
