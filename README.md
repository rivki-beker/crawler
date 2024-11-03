# HtmlSerializer

## Project Description

This project features a tool for HTML processing and querying. It consists of two main parts: HTML serialization and HTML querying.

### Components

1. **Html Serializer** - Serializes HTML into C# objects.
2. **Html Query** - Queries the HTML object tree and searches elements by CSS Selector.

### Html Serializer

- **Reading a Web Page**: Use `HttpClient` to read HTML from a specified URL.
- **Parsing by Tags**: Use `Regular Expressions` to parse the HTML string into parts based on tags and clean up unnecessary spaces.
- **`HtmlElement` Class**: Represents an HTML tag with properties such as Id, Name, Attributes, Classes, InnerHtml, Parent, and Children.
  - **Methods**:
    - `Descendants()` - Returns all descendants of the element.
    - `Ancestors()` - Returns all ancestors of the element.
- **`HtmlHelper` Class**: Implements the Singleton pattern. Loads data from JSON files containing lists of HTML tags and self-closing tags.

### Html Query

- **`Selector` Class**: Represents a CSS Selector with properties like TagName, Id, and Classes.
  - **Method**:
    - `ToSelectorElement` - Converts a Selector string to a `Selector` object.
- **`HtmlElement` Methods**:
  - `SearchBySelector()` - Searches for elements in the tree by CSS Selector and returns a list of matching elements. Uses `HashSet` to prevent duplicates.


## Instructions

1. **Clone the Repository**

   ```bash
   git clone https://github.com/rivki-beker/crawler.git
   ```

2. **Change the URL**

   Update the URL in the `program` file at line 4:

   ```csharp
   var html = await Load("https://www.example.com");
   ```

3. **Update the Query**

   Modify the query in the `program` file at line 48:

   ```csharp
   var res = root.SearchBySelector(Selector.ToSelectorElement("footer p"));
   ```

4. **Run the Project**

   Ensure you have .NET 5.0 or higher installed, then execute:

   ```bash
   dotnet run
   ```

## Contributing

To contribute:
1. Open a Pull Request.
2. Describe the changes clearly.