import React from 'react';
import './App.css';

class App extends React.Component {
	state = {
		value: '0',
		recipes: [],
		ingredients: '',
		showSubComponent: ''
	};

	handleValueChange = (event) => {
    	this.setState({ 
			value: event.target.value
		});
  	}
	
	handleIngredientsChange = (event) => {
		this.setState({ 
			ingredients: event.target.value
		});
	}
	
	showRecipes = () => {
		fetch('http://localhost:9000/api/recipes/get?id=' + this.state.value)
			.then(res => res.json())
			.then(json => this.setState({
				recipes: json,
				showSubComponent: 'showRecipes'
			}));
	}
	
	addExemplaryRecipes = () => {
		fetch('http://localhost:9000/api/recipes/addex')
		.then(this.setState({
			showSubComponent: 'addExemplaryRecipes'
		}));
	}
	
	addRecipe = () => {
		this.setState({ 
			showSubComponent: 'addRecipe'
		});
	}
	
	searchRecipes = () => {
		var listOfIngredients = this.makeTableOfIngredients();
		fetch('http://localhost:9000/api/recipes/search', {
			method: 'POST',
  			headers: {
				Accept: 'application/json',
    			'Content-Type': 'application/json',
  			},
  			body: JSON.stringify({
        		"ListOfIngredients": listOfIngredients
			})
		})
		.then(res => res.json())
		.then(json => this.setState({ 
			recipes: json,
			showSubComponent: 'searchRecipes'
		}));
	}
	
	makeTableOfIngredients = () => {
		var listOfIngredients = this.state.ingredients.split(",").map((item: string) => item.trim());
		return listOfIngredients;	
	}
	
	renderSubComponent = () => {
		switch(this.state.showSubComponent){
            case 'addRecipe': return <AddRecipeComponent />
			case 'showRecipes':
			case 'searchRecipes' : return <RecipesList recipes={this.state.recipes} />
			case 'addExemplaryRecipes': return <div>Recipes added</div>
			default: return ""
        }
			
	}
	render() {
		return(
			<div>
				Select category: 
				<select value={this.state.value} onChange={this.handleValueChange}>
					<option value="0">All</option>
        			<option value="1">Dessert</option>
        			<option value="2">Soup</option>
        			<option value="3">MainCourse</option>
					<option value="4">Snack</option>
      			</select>
				<button value="0" onClick={this.showRecipes}>Show recipes</button>
				<button onClick={this.addExemplaryRecipes}>Add exemplary recipes</button>
				<button onClick={this.addRecipe}>Add your recipe</button>
				<input className="ingredients" placeholder="Ingredients (separated by commas)" value={this.state.ingredients} onChange={this.handleIngredientsChange}></input>
				<button onClick={this.searchRecipes}>Search recipes</button>
				{this.renderSubComponent()}
			</div>
		)
	}
}

class AddRecipeComponent extends React.Component {
	state = {
		name: '',
		category: undefined,
		ingredients: '',
		directions: ''
	}

	handleSelectChange = (event) => {
    	this.setState({ 
			category: event.target.value
		});
  	}
	
	handleNameChange = (event) => {
    	this.setState({ 
			name: event.target.value
		});
  	}
	
	handleDirectionsChange = (event) => {
    	this.setState({ 
			directions: event.target.value
		});
  	}
	
	handleIngredientsChange = (event) => {
    	this.setState({ 
			ingredients: event.target.value
		});
  	}
	
	makeTableOfIngredients = () => {
		var listOfIngredients = this.state.ingredients.split(",").map((item: string) => item.trim());
		return listOfIngredients;	
	}
	
	addRecipe = () => {
		var listOfIngredients = this.makeTableOfIngredients();
		fetch('http://localhost:9000/api/recipes/add', {
			method: 'POST',
  			headers: {
				Accept: 'application/json',
    			'Content-Type': 'application/json',
  			},
  			body: JSON.stringify({
				"Name": this.state.name,
        		"Category": this.state.category,
        		"ListOfIngredients": listOfIngredients,
        		"Directions": this.state.directions
			})
		})
		.then(this.setState({ 
			name: '',
			category: undefined,
			ingredients: '',
			directions: '',
		}));
		
	}
	
	render() {
		return(
			<div>
				<form onSubmit={this.addRecipe} title="Add recipe">
					<div>Add recipe</div>
					<input value={this.state.name} placeholder="Name" onChange={this.handleNameChange}></input>
					<select value={this.state.category} onChange={this.handleSelectChange} title="Select category">
						<option value="1">Dessert</option>
						<option value="2">Soup</option>
						<option value="3">MainCourse</option>
						<option value="4">Snack</option>
      				</select>
					<textarea value={this.state.ingredients} placeholder="Ingredients (separated by commas)" onChange={this.handleIngredientsChange}></textarea>
					<textarea placeholder="Directions" onChange={this.handleDirectionsChange}></textarea>
					<button type="submit" onClick={this.addRecipe}>Add recipe</button>
				</form>
			</div>
		)
	}	
}
	
class RecipesList extends React.Component {
	state = {
		showSubComponent: null,
		recipe: null
	};

	componentWillReceiveProps() {
		this.setState({
			recipe: null,
			showSubComponent: null
		});
	}
	
	showSingleRecipe = (event, recipe) => {
		event.stopPropagation();
		this.setState({
			recipe: recipe,
			showSubComponent: recipe.Id
		});
	}
	
	renderSubComponent = (recipeId) => {
		switch(recipeId){
			case this.state.showSubComponent: return <SingleRecipe recipe={this.state.recipe} />
			default: return ""
		}
	}
	
	render () {
		if (this.props.recipes.length > 0) {
    		return (
				<ul>
        			{this.props.recipes.map(recipe => <li name={recipe.Name} key={recipe.Id.toString()} ingredients={recipe.ListOfIngredients} onClick={event => this.showSingleRecipe(event, recipe)}>{recipe.Name} {this.renderSubComponent(recipe.Id)}</li>)}
      			</ul>
			);
		}
		return (
			<p>No results!</p>
		);
	} 
}

const SingleRecipe = ({recipe}) => {
	if (recipe !== null) {
		return(
			<div className="SingleRecipe">
				<h5>Ingredients:</h5> 
				{recipe.ListOfIngredients.map(ingr => <div key={ingr.toString()}>{ingr}</div>)}
				<h5>Directions:</h5>
				{recipe.Directions}
			</div>
		)
	}
	return (
    <p>No results!</p>
  );
}

export default App;